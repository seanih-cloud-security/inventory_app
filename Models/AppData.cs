using System.ComponentModel;
using InventoryApp.Models;

public class AppData
{
    // Shared inventory instance
    public static Inventory Inventory { get; } = new Inventory(
        new BindingList<Product>(),
        new BindingList<Part>()
    );
}