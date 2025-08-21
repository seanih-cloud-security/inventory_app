using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using InventoryApp.Models;
using InventoryApp.Utils;

namespace InventoryApp.Views;

public partial class ModifyPartView : UserControl
{
    public event EventHandler? CancelClicked;
    public event EventHandler? SaveClicked;
    private Part _part;

    public ModifyPartView(Part part)
    {
        InitializeComponent();
        _part = part;
        DataContext = _part;

        // Update label when radio buttons change
        InHouseRadio.IsCheckedChanged += (_, _) => DynamicLabel.Text = "Company Name";
        OutsourcedRadio.IsCheckedChanged += (_, _) => DynamicLabel.Text = "Machine ID";

        // Populate fields
        IdTextBox.Text = part.PartId.ToString();
        NameTextBox.Text = part.Name;
        InventoryTextBox.Text = part.InStock.ToString();
        PriceTextBox.Text = part.Price.ToString();
        MaxTextBox.Text = part.Max.ToString();
        MinTextBox.Text = part.Min.ToString();

        if (part is InHouse inHousePart)
            DynamicTextBox.Text = inHousePart.MachineId.ToString();
        else if (part is Outsourced outsourcedPart)
            DynamicTextBox.Text = outsourcedPart.CompanyName;
    }

    private async void ModifySaveButton_Click(object? sender, RoutedEventArgs e)
    {
        // TODO: Add Update logic here
        // Read all input values
        int index = AppData.AppInventory.AllParts.IndexOf(_part);
        string name = NameTextBox.Text!;
        string invText = InventoryTextBox.Text!;
        string priceText = PriceTextBox.Text!;
        string minText = MinTextBox.Text!;
        string maxText = MaxTextBox.Text!;
        string machineIdText = DynamicTextBox.Text!; // for InHouse
        string companyName = DynamicTextBox.Text!; // for Outsourced

        // Validate general fields
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

        // Validate InHouse / Outsourced specific fields
        Part updatedPart;

        if (InHouseRadio.IsChecked == true)
        {
            // Machine ID must be integer
            var (machineValid, machineId) = await ValidationHelper.ValidateInt(machineIdText, "Machine ID");
            if (!machineValid) return;

            // Create InHouse part
            updatedPart = new InHouse(index, name, price, inventory, min, max, machineId);
        }
        else
        {
            // Company Name required
            if (!await ValidationHelper.ValidateRequired(companyName, "Company Name")) return;

            // Create Outsourced part
            updatedPart = new Outsourced(index, name, price, inventory, min, max, companyName);
        }

        // Add the new part to the inventory
        AppData.AppInventory.UpdatePart(index, updatedPart);
        Console.WriteLine("ModifySave clicked!");
        SaveClicked?.Invoke(this, EventArgs.Empty);
    }

    private void ModifyCancelButton_Click(object? sender, RoutedEventArgs e)
    {
        CancelClicked?.Invoke(this, EventArgs.Empty);
    }
}