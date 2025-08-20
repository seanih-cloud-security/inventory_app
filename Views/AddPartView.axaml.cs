using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using InventoryApp.Models;
using InventoryApp.Utils;
using MsBox.Avalonia;

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

    private async void SaveButton_Click(object? sender, RoutedEventArgs e)
{
    // 1️⃣ Read all input values
    string name = NameTextBox.Text;
    string invText = InventoryTextBox.Text;
    string priceText = PriceTextBox.Text;
    string minText = MinTextBox.Text;
    string maxText = MaxTextBox.Text;
    string machineIdText = MachineIdOrCompanyNameTextBox.Text;      // for InHouse
    string companyName = MachineIdOrCompanyNameTextBox.Text;       // for Outsourced

    // 2️⃣ Validate general fields
    if (!await ValidationHelper.ValidateRequired(name, "Name")) return;

    var (invValid, inventory) = await ValidationHelper.ValidateInt(invText, "Inventory");
    if (!invValid) return;

    var (minValid, min) = await ValidationHelper.ValidateInt(minText, "Min");
    if (!minValid) return;

    var (maxValid, max) = await ValidationHelper.ValidateInt(maxText, "Max");
    if (!maxValid) return;

    if (min > max)
    {
        await ValidationHelper.ShowError("Min must be ≤ Max.");
        return;
    }

    if (inventory < min || inventory > max)
    {
        await ValidationHelper.ShowError("Inventory must be between Min and Max.");
        return;
    }

    var (priceValid, price) = await ValidationHelper.ValidateDecimal(priceText, "Price", 0);
    if (!priceValid) return;

    // 3️⃣ Validate InHouse / Outsourced specific fields
    Part newPart;

    if (InHouseRadio.IsChecked == true)
    {
        // Machine ID must be integer
        var (machineValid, machineId) = await ValidationHelper.ValidateInt(machineIdText, "Machine ID");
        if (!machineValid) return;

        // Create InHouse part
        newPart = new InHouse(IdGenerator.GeneratePartId(), name, price, inventory, min, max, machineId);
    }
    else
    {
        // Company Name required
        if (!await ValidationHelper.ValidateRequired(companyName, "Company Name")) return;

        // Create Outsourced part
        newPart = new Outsourced(IdGenerator.GeneratePartId(), name, price, inventory, min, max, companyName);
    }

    // 4️⃣ Add the new part to the inventory
    AppData.AppInventory.AddPart(newPart);
    
    SaveClicked?.Invoke(this, EventArgs.Empty);
    
}
    
    private void CancelButton_Click(object? sender, RoutedEventArgs e)
    {
        CancelClicked?.Invoke(this, EventArgs.Empty);
    }
}