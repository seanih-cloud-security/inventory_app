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
    
    public AddPartView()
    {
        InitializeComponent();
        
        // Update label when radio buttons change
        InHouseRadio.IsCheckedChanged += (_, _) => DynamicLabel.Text = "Company Name";
        OutsourcedRadio.IsCheckedChanged += (_, _) => DynamicLabel.Text = "Machine ID";
    }

    public void SaveButton_Click(object? sender, RoutedEventArgs e)
    {
        // TODO: Add saving logic here
        // 1. Create new Part from user input
        if (InHouseRadio.IsChecked == true)
        {
            InHouse newPart = new InHouse(
                partId: IdGenerator.GeneratePartId(),
                name: NameTextBox.Text,
                price: decimal.Parse(PriceTextBox.Text),
                inStock: int.Parse(InventoryTextBox.Text),
                min: int.Parse(MinTextBox.Text),
                max: int.Parse(MaxTextBox.Text),
                machineId: int.Parse(MachineIdOrCompanyNameTextBox.Text)
            );
            
            AppData.AppInventory.AddPart(newPart);
            Console.WriteLine("Inhouse Part Added");
        }
        else
        {
            Outsourced newPart = new Outsourced(
                partId: IdGenerator.GeneratePartId(),
                name: NameTextBox.Text,
                price: decimal.Parse(PriceTextBox.Text),
                inStock: int.Parse(InventoryTextBox.Text),
                min: int.Parse(MinTextBox.Text),
                max: int.Parse(MaxTextBox.Text),
                companyName: MachineIdOrCompanyNameTextBox.Text
            );
            
            AppData.AppInventory.AddPart(newPart);
            Console.WriteLine("Outsourced Part Added");
        }
        
        SaveClicked?.Invoke(this, EventArgs.Empty);
    }

    private void CancelButton_Click(object? sender, RoutedEventArgs e)
    {
        CancelClicked?.Invoke(this, EventArgs.Empty);
    }
}