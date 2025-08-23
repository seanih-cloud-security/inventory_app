// using Avalonia.Controls;
// using Avalonia.Interactivity;
// using System;
//
// namespace InventoryApp.Views
// {
//     public partial class AddProductView : UserControl
//     {
//         public event EventHandler? CancelClicked;
//         public event EventHandler? SaveClicked;
//
//         // Constructor
//         public AddProductView()
//         {
//             InitializeComponent();
//         }
//
//         // add event handlers here
//         private void AddProductSaveButton_Click(object? sender, RoutedEventArgs e)
//         {
//             SaveClicked?.Invoke(this, EventArgs.Empty);
//             // TODO: Add saving logic here
//         }
//
//         private void AddProductCancelButton_Click(object? sender, RoutedEventArgs e)
//         {
//             CancelClicked?.Invoke(this, EventArgs.Empty);
//         }
//     }
// }

using System;
using System.Collections.ObjectModel;
using Avalonia.Controls;
using Avalonia.Interactivity;
using InventoryApp.Models;
using InventoryApp.Utils; // assuming your Part/Product classes live here
using MsBox.Avalonia; // for AppData.AppInventory

namespace InventoryApp.Views
{
    public partial class AddProductView : UserControl
    {
        public event EventHandler? CancelClicked;
        public event EventHandler<Product>? SaveClicked;

        private ObservableCollection<Part> _associatedParts = new ObservableCollection<Part>();

        public AddProductView()
        {
            InitializeComponent();

            // Bind grids
            var allPartsGrid = this.FindControl<DataGrid>("AllPartsDataGrid");
            var assocPartsGrid = this.FindControl<DataGrid>("AssociatedPartsDataGrid");

            allPartsGrid.ItemsSource = AppData.AppInventory.AllParts;
            assocPartsGrid.ItemsSource = _associatedParts;

            // Wire buttons
            this.FindControl<Button>("AddPartToProductBtn")!.Click += AddAssocPartButton_Click;
            this.FindControl<Button>("DeletePartFromProductBtn")!.Click += DeleteAssocPartButton_Click;
        }

        private void AddAssocPartButton_Click(object? sender, RoutedEventArgs e)
        {
            var allPartsGrid = this.FindControl<DataGrid>("AllPartsDataGrid");
            if (allPartsGrid.SelectedItem is Part selectedPart && !_associatedParts.Contains(selectedPart))
            {
                _associatedParts.Add(selectedPart);
            }
        }

        private void DeleteAssocPartButton_Click(object? sender, RoutedEventArgs e)
        {
            var assocPartsGrid = this.FindControl<DataGrid>("AssociatedPartsDataGrid");
            if (assocPartsGrid.SelectedItem is Part selectedPart)
            {
                _associatedParts.Remove(selectedPart);
            }
        }

        private async void AddProductSaveButton_Click(object? sender, RoutedEventArgs e)
        {
            try
            {
                // Read all input values
                string name = NameTextBox.Text!;
                string invText = InventoryTextBox.Text!;
                string priceText = PriceTextBox.Text!;
                string minText = MinTextBox.Text!;
                string maxText = MaxTextBox.Text!;

                // Validate general fields
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

                var newProduct = new Product(
                    new ObservableCollection<Part>(),
                    IdGenerator.GenerateProductID(),
                    name,
                    price,
                    inventory,
                    min,
                    max
                );
                
                AppData.AppInventory.AddProduct(newProduct);

                foreach (var part in _associatedParts)
                    newProduct.AddAssociatedPart(part);

                SaveClicked?.Invoke(this, newProduct);
            }
            catch (Exception)
            {
                await MessageBoxManager.GetMessageBoxStandard("Error", "Invalid input, please check all fields")
                    .ShowAsync();
            }
        }
        
        private void AddProductCancelButton_Click(object? sender, RoutedEventArgs e)
        {
            CancelClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}