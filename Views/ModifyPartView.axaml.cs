using Avalonia.Controls;
using Avalonia.Interactivity;
using System;

namespace InventoryApp.Views;

public partial class ModifyPartView : UserControl
{
    public event EventHandler? CancelClicked;
    public event EventHandler? SaveClicked;

    public ModifyPartView()
    {
        InitializeComponent();

        // Update label when radio buttons change
        InHouseRadio.IsCheckedChanged += (_, _) => DynamicLabel.Text = "Company Name:";
        OutsourcedRadio.IsCheckedChanged += (_, _) => DynamicLabel.Text = "Machine ID:";
    }

    private void ModifySaveButton_Click(object? sender, RoutedEventArgs e)
    {
        SaveClicked?.Invoke(this, EventArgs.Empty);
        // TODO: Add saving logic here
    }

    private void ModifyCancelButton_Click(object? sender, RoutedEventArgs e)
    {
        CancelClicked?.Invoke(this, EventArgs.Empty);
    }
}