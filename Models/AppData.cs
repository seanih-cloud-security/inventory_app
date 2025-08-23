using System;
using System.Collections.Generic;
using System.Linq;

namespace InventoryApp.Models;

using System.Collections.ObjectModel;

public static class AppData
{
    // Shared inventory instance
    public static Inventory AppInventory { get; } = new Inventory(
        new ObservableCollection<Product>(),
        new ObservableCollection<Part>()
    );
}