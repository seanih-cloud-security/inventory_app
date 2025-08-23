using Avalonia.Controls;
using System;
using InventoryApp.Models;

namespace InventoryApp.Views;

public partial class MainWindow : Window
{
    private readonly InventoryView _inventoryView;
    private AddPartView? _addPartView;
    private ModifyPartView? _modifyPartView;
    private AddProductView? _addProductView;
    private ModifyProductView? _modifyProductView;

    public MainWindow()
    {
        InitializeComponent();

        // Start with the Inventory view
        _inventoryView = new InventoryView();
        _inventoryView.AddPartClicked += InventoryView_AddPartClicked;
        _inventoryView.ModifyPartClicked += InventoryView_ModifyPartClicked;
        _inventoryView.AddProductClicked += InventoryView_AddProductClicked;
        _inventoryView.ModifyProductClicked += InventoryView_ModifyProductClicked;
        _inventoryView.ExitClicked += InventoryView_ExitClicked;

        NavigateTo(_inventoryView);
    }

    // --- Small helpers to reduce repetition ---
    private void NavigateTo(UserControl view)
    {
        MainContent.Content = view;
    }

    private void ReturnToInventory()
    {
        // Show inventory
        NavigateTo(_inventoryView);

        // Detach events & clear references to prevent leaks
        if (_addPartView is not null)
        {
            _addPartView.CancelClicked -= AddPartView_CancelClicked;
            _addPartView.SaveClicked -= AddPartView_SaveClicked;
            _addPartView = null;
        }

        if (_modifyPartView is not null)
        {
            _modifyPartView.CancelClicked -= ModifyPartView_CancelClicked;
            _modifyPartView.SaveClicked -= ModifyPartView_SaveClicked;
            _modifyPartView = null;
        }

        if (_addProductView is not null)
        {
            _addProductView.CancelClicked -= AddProductView_CancelClicked;
            _addProductView.SaveClicked -= AddProductView_SaveClicked;
            _addProductView = null;
        }

        if (_modifyProductView is not null)
        {
            _modifyProductView.CancelClicked -= ModifyProductView_CancelClicked;
            _modifyProductView.SaveClicked -= ModifyProductView_SaveClicked;
            _modifyProductView = null;
        }
    }

    // --- Inventory -> other views ---
    private void InventoryView_AddPartClicked(object? sender, EventArgs e)
    {
        _addPartView = new AddPartView();
        _addPartView.CancelClicked += AddPartView_CancelClicked;
        _addPartView.SaveClicked += AddPartView_SaveClicked;
        NavigateTo(_addPartView);
    }

    private void InventoryView_ModifyPartClicked(Part selectedPart)
    {
        _modifyPartView = new ModifyPartView(selectedPart);
        _modifyPartView.CancelClicked += ModifyPartView_CancelClicked;
        _modifyPartView.SaveClicked += ModifyPartView_SaveClicked;
        NavigateTo(_modifyPartView);
    }

    private void InventoryView_AddProductClicked(object? sender, EventArgs e)
    {
        _addProductView = new AddProductView();
        _addProductView.CancelClicked += AddProductView_CancelClicked;
        _addProductView.SaveClicked += AddProductView_SaveClicked;
        NavigateTo(_addProductView);
    }

    private void InventoryView_ModifyProductClicked(object? sender, EventArgs e)
    {
        _modifyProductView = new ModifyProductView();
        _modifyProductView.CancelClicked += ModifyProductView_CancelClicked;
        _modifyProductView.SaveClicked += ModifyProductView_SaveClicked;
        NavigateTo(_modifyProductView);
    }

    private void InventoryView_ExitClicked(object? sender, EventArgs e)
    {
        Close();
    }

    // --- Save/Cancel from child views -> back to inventory ---
    private void AddPartView_SaveClicked(object? sender, EventArgs e) => ReturnToInventory();
    private void AddPartView_CancelClicked(object? sender, EventArgs e) => ReturnToInventory();

    private void ModifyPartView_SaveClicked(object? sender, EventArgs e) => ReturnToInventory();
    private void ModifyPartView_CancelClicked(object? sender, EventArgs e) => ReturnToInventory();

    private void AddProductView_SaveClicked(object? sender, Product newProduct) => ReturnToInventory();
    private void AddProductView_CancelClicked(object? sender, EventArgs e) => ReturnToInventory();

    private void ModifyProductView_SaveClicked(object? sender, EventArgs e) => ReturnToInventory();
    private void ModifyProductView_CancelClicked(object? sender, EventArgs e) => ReturnToInventory();
}