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
    
    public static ObservableCollection<Part> FilteredParts { get; } = new ObservableCollection<Part>();
    
    // public static void RefreshFilteredParts(string? searchTerm = null)
    // {
    //     FilteredParts.Clear();
    //
    //     IEnumerable<Part> source = AppInventory.AllParts;
    //
    //     if (!string.IsNullOrWhiteSpace(searchTerm))
    //         source = source.Where(p => p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
    //
    //     foreach (var part in source)
    //         FilteredParts.Add(part);
    // }
}