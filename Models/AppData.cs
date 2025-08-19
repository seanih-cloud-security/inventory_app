namespace InventoryApp.Models;

using System.Collections.ObjectModel;
using System.ComponentModel;

public static class AppData
{
    // Shared inventory instance
    public static Inventory AppInventory { get; } = new Inventory(
        new ObservableCollection<Product>(),
        new ObservableCollection<Part>()
    );
}