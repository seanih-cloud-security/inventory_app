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
        InHouseRadio.Checked += (_, _) => DynamicLabel.Text = "Machine ID:";
        OutsourcedRadio.Checked += (_, _) => DynamicLabel.Text = "Company Name:";
    }

    private void SaveButton_Click(object? sender, RoutedEventArgs e)
    {
        SaveClicked?.Invoke(this, EventArgs.Empty);
        // TODO: Add saving logic here
    }

    private void CancelButton_Click(object? sender, RoutedEventArgs e)
    {
        CancelClicked?.Invoke(this, EventArgs.Empty);
    }
}