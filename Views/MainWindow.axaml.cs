using Avalonia.Controls;
using Avalonia.Interactivity;
using System;

namespace InventoryApp.Views;

public partial class MainWindow : Window
{
    private InventoryView _inventoryView;
    private AddPartView? _addPartView;

    public MainWindow()
    {
        InitializeComponent();

        _inventoryView = new InventoryView();
        _inventoryView.AddPartClicked += InventoryView_AddPartClicked;
        _inventoryView.ExitClicked += InventoryView_ExitClicked;

        MainContent.Content = _inventoryView;
    }

    private void InventoryView_AddPartClicked(object? sender, EventArgs e)
    {
        // TODO: Load AddPartView here later
        _addPartView = new AddPartView();
        _addPartView.CancelClicked += AddPartView_CancelClicked;
        _addPartView.SaveClicked += AddPartView_SaveClicked;
        MainContent.Content = _addPartView;
    }

    private void InventoryView_ExitClicked(object? sender, EventArgs e)
    {
        Close();
    }
    
    private void AddPartView_CancelClicked(object? sender, EventArgs e)
    {
        // Return to inventory view
        MainContent.Content = _inventoryView;
        _addPartView = null;
    }
    
    private void AddPartView_SaveClicked(object? sender, EventArgs e)
    {
        // TODO: Implement saving logic

        // After saving, go back to inventory view
        MainContent.Content = _inventoryView;
        _addPartView = null;
    }
}