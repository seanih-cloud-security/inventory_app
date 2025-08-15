using Avalonia.Controls;
using Avalonia.Interactivity;
using System;

namespace InventoryApp.Views;

public partial class AddPartView : UserControl
{
    public event EventHandler? CancelClicked;
    public event EventHandler? SaveClicked;

    public AddPartView()
    {
        InitializeComponent();

        // Update label when radio buttons change
        InHouseRadio.IsCheckedChanged += (_, _) => DynamicLabel.Text = "Company Name:";
        OutsourcedRadio.IsCheckedChanged += (_, _) => DynamicLabel.Text = "Machine ID:";
    }

    private void SaveButton_Click(object? sender, RoutedEventArgs e)
    {
        // TODO: Add saving logic here
        
        
        SaveClicked?.Invoke(this, EventArgs.Empty);
    }

    private void CancelButton_Click(object? sender, RoutedEventArgs e)
    {
        CancelClicked?.Invoke(this, EventArgs.Empty);
    }
}