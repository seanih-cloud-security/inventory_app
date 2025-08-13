using Avalonia.Controls;
using System;

namespace InventoryApp.Views;

public partial class MainWindow : Window
{
    private InventoryView _inventoryView;
    private AddPartView? _addPartView;
    private ModifyPartView? _modifyPartView;

    public MainWindow()
    {
        InitializeComponent();

        _inventoryView = new InventoryView();
        _inventoryView.AddPartClicked += InventoryView_AddPartClicked;
        _inventoryView.ModifyPartClicked += InventoryView_ModifyPartClicked;
        _inventoryView.ExitClicked += InventoryView_ExitClicked;
        
        

        MainContent.Content = _inventoryView;
    }

    private void InventoryView_AddPartClicked(object? sender, EventArgs e)
    {
        // TODO: Load AddPartView
        _addPartView = new AddPartView();
        _addPartView.CancelClicked += AddPartView_CancelClicked;
        _addPartView.SaveClicked += AddPartView_SaveClicked;
        MainContent.Content = _addPartView;
    }
    
    private void InventoryView_ModifyPartClicked(object? sender, EventArgs e)
    {
        // TODO: Load ModifyPartView
        _modifyPartView = new ModifyPartView();
        _modifyPartView.CancelClicked += ModifyPartView_CancelClicked;
        _modifyPartView.SaveClicked += ModifyPartView_SaveClicked;
        MainContent.Content = _modifyPartView;
    }

    private void InventoryView_ExitClicked(object? sender, EventArgs e)
    {
        Close();
    }
    
    private void AddPartView_SaveClicked(object? sender, EventArgs e)
    {
        // TODO: Implement saving logic

        // After saving, go back to inventory view
        MainContent.Content = _inventoryView;
        _addPartView = null;
    }
    
    private void AddPartView_CancelClicked(object? sender, EventArgs e)
    {
        // Return to inventory view
        MainContent.Content = _inventoryView;
        _addPartView = null;
    }
    
    private void ModifyPartView_SaveClicked(object? sender, EventArgs e)
    {
        // TODO: Implement saving logic

        // After saving, go back to inventory view
        MainContent.Content = _inventoryView;
        _addPartView = null;
    }
    
    private void ModifyPartView_CancelClicked(object? sender, EventArgs e)
    {
        // Return to inventory view
        MainContent.Content = _inventoryView;
        _modifyPartView = null;
    }
    
    
}