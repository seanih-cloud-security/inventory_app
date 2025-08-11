using Avalonia.Controls;
using Avalonia.Interactivity; // For RoutedEventArgs
using System;

namespace InventoryApp.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    // Example handler for Add button
    private void AddPartButton_Click(object? sender, RoutedEventArgs e)
    {
        Console.WriteLine("Add button clicked");
        // TODO: Add your add logic here
    }

    // Similarly, add handlers for other buttons...
    private void ModifyPartButton_Click(object? sender, RoutedEventArgs e)
    {
        Console.WriteLine("Modify button clicked");
    }

    private void DeletePartButton_Click(object? sender, RoutedEventArgs e)
    {
        Console.WriteLine("Delete button clicked");
    }
    
    private void AddProductButton_Click(object? sender, RoutedEventArgs e)
    {
        Console.WriteLine("Add button clicked");
        // TODO: Add your add logic here
    }

    // Similarly, add handlers for other buttons...
    private void ModifyProductButton_Click(object? sender, RoutedEventArgs e)
    {
        Console.WriteLine("Modify button clicked");
    }

    private void DeleteProductButton_Click(object? sender, RoutedEventArgs e)
    {
        Console.WriteLine("Delete button clicked");
    }

    private void ExitButton_Click(object? sender, RoutedEventArgs e)
    {
        Close(); // closes the window
    }
}