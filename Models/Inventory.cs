using System.ComponentModel;
using System.Linq;
using System.Collections.ObjectModel;

namespace InventoryApp.Models;

public class Inventory
{
    public ObservableCollection<Product> Products { get; init; } = new();
    public ObservableCollection<Part> AllParts { get; set; } = new();

    // constructor can optionally take initial lists
    public Inventory(ObservableCollection<Product>? products = null, ObservableCollection<Part>? allParts = null)
    {
        Products = products ?? new();
        AllParts = allParts ?? new();
    }

    // ===== PRODUCT METHODS =====
    public void AddProduct(Product product) => Products.Add(product);

    public bool RemoveProduct(int productId)
    {
        var product = Products.FirstOrDefault(p => p.ProductId == productId);
        if (product == null) return false;

        Products.Remove(product);
        return true;
    }

    public Product? LookupProduct(int productId) =>
        Products.FirstOrDefault(p => p.ProductId == productId);

    public void UpdateProduct(int index, Product newProduct)
    {
        if (index >= 0 && index < Products.Count)
        {
            Products[index].CopyFrom(newProduct);
        }
    }

    // ===== PART METHODS =====
    public void AddPart(Part part) => AllParts.Add(part);

    public bool RemovePart(int partId)
    {
        var part = AllParts.FirstOrDefault(p => p.PartId == partId);
        if (part == null) return false;

        AllParts.Remove(part);
        return true;
    }

    public Part? LookupPart(int partId) =>
        AllParts.FirstOrDefault(p => p.PartId == partId);

    public void UpdatePart(int index, Part newPart)
    {
        if (index >= 0 && index < AllParts.Count)
        {
            AllParts[index].CopyFrom(newPart);
        }
    }
}