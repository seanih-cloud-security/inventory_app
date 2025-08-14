using Avalonia.Controls;
using Avalonia.Interactivity;
using System;

namespace InventoryApp.Views;

public partial class InventoryView : UserControl
{
    public event EventHandler? AddPartClicked;
    public event EventHandler? AddProductClicked;
    public event EventHandler? ModifyPartClicked;
    public event EventHandler? ExitClicked;

    public InventoryView()
    {
        InitializeComponent();
    }

    private void AddPartButton_Click(object? sender, RoutedEventArgs e)
    {
        AddPartClicked?.Invoke(this, EventArgs.Empty);
    }
    
    // Implement other button handlers similarly or add events
    private void ModifyPartButton_Click(object? sender, RoutedEventArgs e)
    {
        // TODO: Add modify part logic here
        Console.WriteLine("ModifyPartButton_Click");
        ModifyPartClicked?.Invoke(this, EventArgs.Empty);
    }

    private void DeletePartButton_Click(object? sender, RoutedEventArgs e)
    {
        // TODO: Add delete part logic here
        Console.WriteLine("DeletePartButton_Click");
    }

    private void AddProductButton_Click(object? sender, RoutedEventArgs e)
    {
        // TODO: Add add product logic here
        Console.WriteLine("AddProductButton_Click");
        AddProductClicked?.Invoke(this, EventArgs.Empty);
    }

    private void ModifyProductButton_Click(object? sender, RoutedEventArgs e)
    {
        // TODO: Add modify product logic here
        Console.WriteLine("ModifyProductButton_Click");
    }

    private void DeleteProductButton_Click(object? sender, RoutedEventArgs e)
    {
        // TODO: Add delete product logic here
        Console.WriteLine("Delete product clicked");
    }
    
    private void ExitButton_Click(object? sender, RoutedEventArgs e)
    {
        ExitClicked?.Invoke(this, EventArgs.Empty);
    }

}