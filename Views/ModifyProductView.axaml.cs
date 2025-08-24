using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using InventoryApp.Models;
using InventoryApp.Utils;
using MsBox.Avalonia;

namespace InventoryApp.Views
{
    public partial class ModifyProductView : UserControl
    {
        public event EventHandler? CancelClicked;
        public event EventHandler? SaveClicked;

        private Product _product; // original inventory reference
        private ObservableCollection<Part> _associatedParts; // bound to DataGrid for UI edits

        public ModifyProductView(Product selectedProduct)
        {
            InitializeComponent();

            // Keep the original product reference
            _product = selectedProduct;

            // Create a detached collection for UI binding
            _associatedParts = new ObservableCollection<Part>(selectedProduct.AssociatedParts);

            // Bind the product fields to the form
            IdTextBox.Text = _product.ProductId.ToString();
            NameTextBox.Text = _product.Name;
            InventoryTextBox.Text = _product.InStock.ToString();
            PriceTextBox.Text = _product.Price.ToString();
            MaxTextBox.Text = _product.Max.ToString();
            MinTextBox.Text = _product.Min.ToString();

            // Bind DataGrids
            AllPartsDataGrid.ItemsSource = AppData.AppInventory.AllParts;
            AssociatedPartsDataGrid.ItemsSource = _associatedParts;
        }

        private async void ModifyProductSaveButton_Click(object? sender, RoutedEventArgs e)
        {
            string name = NameTextBox.Text!;
            string invText = InventoryTextBox.Text!;
            string priceText = PriceTextBox.Text!;
            string minText = MinTextBox.Text!;
            string maxText = MaxTextBox.Text!;

            // Validate fields
            if (!await ValidationHelper.ValidateRequired(name, "Name")) return;

            var (invValid, inventory) = await ValidationHelper.ValidateInt(invText, "Inventory");
            if (!invValid) return;

            var (minValid, min) = await ValidationHelper.ValidateInt(minText, "Min");
            if (!minValid) return;

            var (maxValid, max) = await ValidationHelper.ValidateInt(maxText, "Max");
            if (!maxValid) return;

            if (min > max)
            {
                await ValidationHelper.ShowError("Min must be ≤ Max.");
                return;
            }

            if (inventory < min || inventory > max)
            {
                await ValidationHelper.ShowError("Inventory must be between Min and Max.");
                return;
            }

            var (priceValid, price) = await ValidationHelper.ValidateDecimal(priceText, "Price", 0);
            if (!priceValid) return;

            // Update the original product
            _product.Name = name;
            _product.Price = price;
            _product.InStock = inventory;
            _product.Min = min;
            _product.Max = max;

            // Update the associated parts
            _product.AssociatedParts = new ObservableCollection<Part>(_associatedParts.ToList());

            SaveClicked?.Invoke(this, EventArgs.Empty);
        }

        private void ModifyProductCancelButton_Click(object? sender, RoutedEventArgs e)
        {
            CancelClicked?.Invoke(this, EventArgs.Empty);
        }
        
        private async void SearchPartButton_Click(object? sender, RoutedEventArgs e)
        {
            string searchText = SearchTextBox.Text?.Trim() ?? "";
            ObservableCollection<Part> allParts = AppData.AppInventory.AllParts;

            if (string.IsNullOrWhiteSpace(searchText))
            {
                // Reset to show all parts
                AllPartsDataGrid.ItemsSource = allParts;
                return;
            }

            // Lookup by name
            var part = AppData.AppInventory.LookupPartByName(searchText);

            if (part != null)
            {
                AllPartsDataGrid.ItemsSource = new List<Part> { part };
            }
            else
            {
                var noResults = MessageBoxManager.GetMessageBoxStandard(
                    "Search",
                    "No results match."
                );
                await noResults.ShowAsync();
                // Reset
                AllPartsDataGrid.ItemsSource = AppData.AppInventory.AllParts;
            }
        }


        private void AddAssocPartButton_Click(object? sender, RoutedEventArgs e)
        {
            var selectedPart = AllPartsDataGrid.SelectedItem as Part;
            if (selectedPart != null && !_associatedParts.Contains(selectedPart))
            {
                _associatedParts.Add(selectedPart);
            }
        }

        // private void DeleteAssocPartButton_Click(object? sender, RoutedEventArgs e)
        // {
        //     var assocPartsGrid = this.FindControl<DataGrid>("AssociatedPartsDataGrid");
        //     if (assocPartsGrid.SelectedItem is Part selectedPart)
        //     {
        //         _associatedParts.Remove(selectedPart);
        //     }
        // }
        
        private void DeleteAssocPartButton_Click(object? sender, RoutedEventArgs e)
        {
            var assocPartsGrid = this.FindControl<DataGrid>("AssociatedPartsDataGrid");
    
            Console.WriteLine($"=== DELETE BUTTON CLICKED ===");
            Console.WriteLine($"Grid has selection: {assocPartsGrid.SelectedItem != null}");
            Console.WriteLine($"Collection count before: {_associatedParts.Count}");
    
            // Log all current parts
            for (int i = 0; i < _associatedParts.Count; i++)
            {
                Console.WriteLine($"  [{i}] {_associatedParts[i].PartId} - {_associatedParts[i].Name}");
            }
    
            if (assocPartsGrid.SelectedItem is Part selectedPart)
            {
                Console.WriteLine($"Selected part: {selectedPart.PartId} - {selectedPart.Name}");
        
                bool removed = _associatedParts.Remove(selectedPart);
        
                Console.WriteLine($"Remove() returned: {removed}");
                Console.WriteLine($"Collection count after: {_associatedParts.Count}");
        
                // Log remaining parts
                for (int i = 0; i < _associatedParts.Count; i++)
                {
                    Console.WriteLine($"  Remaining [{i}] {_associatedParts[i].PartId} - {_associatedParts[i].Name}");
                }
            }
            else
            {
                Console.WriteLine("No item selected!");
            }
        }
    }
}