using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using InventoryApp.Models;
using InventoryApp.Utils;

namespace InventoryApp.Views;

public partial class AddPartView : UserControl
{
    public event EventHandler? CancelClicked;
    public event EventHandler? SaveClicked;
    
    // Generate a new ID and set it as the default value
    int _newId = IDGenerator.GeneratePartId();
    public AddPartView()
    {
        InitializeComponent();
        
        PartIdTextBox.Text = _newId.ToString();

        // Optionally disable editing if IDs should never be changed
        PartIdTextBox.IsReadOnly = true;

        // Update label when radio buttons change
        InHouseRadio.IsCheckedChanged += (_, _) => DynamicLabel.Text = "Company Name:";
        OutsourcedRadio.IsCheckedChanged += (_, _) => DynamicLabel.Text = "Machine ID:";
    }

    public void SaveButton_Click(object? sender, RoutedEventArgs e)
    {
        // TODO: Add saving logic here
        // 1. Create new Part from user input
        if (InHouseRadio.IsChecked == true)
        {
            Inhouse newPart = new Inhouse(
                partId: _newId,
                name: NameTextBox.Text,
                price: decimal.Parse(PriceTextBox.Text),
                inStock: int.Parse(InventoryTextBox.Text),
                min: int.Parse(MinTextBox.Text),
                max: int.Parse(MaxTextBox.Text),
                machineId: int.Parse(MachineIdOrCompanyNameTextBox.Text)
            );
            
            AppData.Inventory.AddPart(newPart);
            Console.WriteLine("Inhouse Part Added");
        }
        else
        {
            Outsourced newPart = new Outsourced(
                partId: _newId,
                name: NameTextBox.Text,
                price: decimal.Parse(PriceTextBox.Text),
                inStock: int.Parse(InventoryTextBox.Text),
                min: int.Parse(MinTextBox.Text),
                max: int.Parse(MaxTextBox.Text),
                companyName: MachineIdOrCompanyNameTextBox.Text
            );
            
            AppData.Inventory.AddPart(newPart);
            Console.WriteLine("Outsourced Part Added");
        }
        
        SaveClicked?.Invoke(this, EventArgs.Empty);
    }

    private void CancelButton_Click(object? sender, RoutedEventArgs e)
    {
        CancelClicked?.Invoke(this, EventArgs.Empty);
    }
}