using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using InventoryApp.Models;
using InventoryApp.Utils;
using MsBox.Avalonia;

namespace InventoryApp.Views;

public partial class InventoryView : UserControl
{
    public event EventHandler? AddPartClicked;
    public event Action<Part>? ModifyPartClicked;
    public event EventHandler? AddProductClicked;
    public event Action<Product>? ModifyProductClicked;
    public event EventHandler? ExitClicked;

    public InventoryView()
    {
        InitializeComponent();
        DataContext = AppData.AppInventory;

        // Initialize FilteredParts with everything
        AppData.AppInventory.RefreshFilteredParts();
    }

    private void AddPartButton_Click(object? sender, RoutedEventArgs e)
    {
        AddPartClicked?.Invoke(this, EventArgs.Empty);
    }

    // Implement other button handlers similarly or add events
    private void ModifyPartButton_Click(object? sender, RoutedEventArgs e)
    {
        // TODO: Add modify part logic here
        if (PartsDataGrid.SelectedItem is Part selectedPart)
        {
            ModifyPartClicked?.Invoke(selectedPart);
        }
        else
        {
            Console.WriteLine("No part selected to modify.");
        }
    }

    private async void DeletePartButton_Click(object? sender, RoutedEventArgs e)
    {
        if (PartsDataGrid.SelectedItem is not Part selectedPart)
        {
            await ValidationHelper.ShowError("Please select a part to delete.");
            return;
        }

        bool confirm = await ValidationHelper.ShowConfirmation(
            $"Are you sure you want to delete '{selectedPart.Name}'?",
            "Delete Part");

        if (confirm)
        {
            AppData.AppInventory.RemovePart(selectedPart.PartId);
            Console.WriteLine($"Part {selectedPart.Name} deleted.");
        }
    }

    private async void SearchPartButton_Click(object? sender, RoutedEventArgs e)
    {
        string searchText = PartsSearchBox.Text?.Trim() ?? "";
        ObservableCollection<Part> allParts = AppData.AppInventory.AllParts;

        if (string.IsNullOrWhiteSpace(searchText))
        {
            // Reset to show all parts
            PartsDataGrid.ItemsSource = allParts;
            return;
        }

        // Lookup by name
        var part = AppData.AppInventory.LookupPartByName(searchText);

        if (part != null)
        {
            PartsDataGrid.ItemsSource = new List<Part> { part };
        }
        else
        {
            var noResults = MessageBoxManager.GetMessageBoxStandard(
                "Search",
                "No results match."
            );
            await noResults.ShowAsync();
            // Reset
            PartsDataGrid.ItemsSource = AppData.AppInventory.AllParts;
        }
    }

    private void AddProductButton_Click(object? sender, RoutedEventArgs e)
    {
        // TODO: Add add product logic here
        AddProductClicked?.Invoke(this, EventArgs.Empty);
    }

    private void ModifyProductButton_Click(object? sender, RoutedEventArgs e)
    {
        // TODO: Add modify product logic here
        if (ProductsDataGrid.SelectedItem is Product selectedProduct)
        {
            ModifyProductClicked?.Invoke(selectedProduct);
        }
        else
        {
            Console.WriteLine("No product selected to modify.");
        }
    }

    private async void DeleteProductButton_Click(object? sender, RoutedEventArgs e)
    {
        if (ProductsDataGrid.SelectedItem is not Product selectedProduct)
        {
            await ValidationHelper.ShowError("Please select a part to delete.");
            return;
        }

        bool confirm = await ValidationHelper.ShowConfirmation(
            $"Are you sure you want to delete '{selectedProduct.Name}'?",
            "Delete Part");

        if (confirm)
        {
            AppData.AppInventory.RemoveProduct(selectedProduct.ProductId);
            Console.WriteLine($"Part {selectedProduct.Name} deleted.");
        }
    }

    private async void SearchProductButton_Click(object? sender, RoutedEventArgs e)
    {
        string searchText = ProductsSearchBox.Text?.Trim() ?? "";

        if (string.IsNullOrWhiteSpace(searchText))
        {
            // Reset to show all parts
            ProductsDataGrid.ItemsSource = AppData.AppInventory.Products;
            return;
        }

        // Lookup by name
        var product = AppData.AppInventory.LookupProductByName(searchText);

        if (product != null)
        {
            ProductsDataGrid.ItemsSource = new List<Product> { product };
        }
        else
        {
            var noResults = MessageBoxManager.GetMessageBoxStandard(
                "Search",
                "No results match."
            );
            await noResults.ShowAsync();
            // Reset
            ProductsDataGrid.ItemsSource = AppData.AppInventory.Products;
        }
    }


    private void ExitButton_Click(object? sender, RoutedEventArgs e)
    {
        ExitClicked?.Invoke(this, EventArgs.Empty);
    }
}