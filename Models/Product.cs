using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace InventoryApp.Models;

public class Product(
    ObservableCollection<Part> associatedParts,
    int productId,
    string name,
    decimal price,
    int inStock,
    int min,
    int max)
{
    public ObservableCollection<Part> AssociatedParts { get; set; } = associatedParts;
    public int ProductId { get; set; } = productId;
    public string Name { get; set; } = name;
    public decimal Price { get; set; } = price;
    public int InStock { get; set; } = inStock;
    public int Min { get; set; } = min;
    public int Max { get; set; } = max;

    public void CopyFrom(Product other)
    {
        ProductId = other.ProductId;
        Name = other.Name;
        Price = other.Price;
        InStock = other.InStock;
        Min = other.Min;
        Max = other.Max;
    }

    public void AddAssociatedPart(Part associatedPart)
    {
        AssociatedParts.Add(associatedPart);
    }

    public bool RemoveAssociatedPart(int partId)
    {
        var part = AssociatedParts.FirstOrDefault(p => p.PartId == partId);
        if (part != null)
        {
            AssociatedParts.Remove(part);
            return true;
        }

        return false;
    }

    public Part? LookupAssociatedPart(int partId)
    {
        return AssociatedParts.FirstOrDefault(p => p.PartId == partId);
    }
}