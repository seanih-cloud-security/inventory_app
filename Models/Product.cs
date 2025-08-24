using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace InventoryApp.Models;

public class Product
{
    public ObservableCollection<Part> AssociatedParts { get; set; } = new ObservableCollection<Part>();
    public int ProductId { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public int InStock { get; set; }
    public int Min { get; set; }
    public int Max { get; set; }

    public Product(int productId, string name, decimal price, int inStock, int min, int max)
    {
        ProductId = productId;
        Name = name;
        Price = price;
        InStock = inStock;
        Min = min;
        Max = max;
    }

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