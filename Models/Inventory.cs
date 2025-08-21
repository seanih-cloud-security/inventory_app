using System.Linq;
using System.Collections.ObjectModel;

namespace InventoryApp.Models;

public class Inventory
{
    public ObservableCollection<Product> Products { get; set; }
    public ObservableCollection<Part> AllParts { get; set; }

    // constructor can optionally take initial lists
    public Inventory(ObservableCollection<Product>? products = null, ObservableCollection<Part>? allParts = null)
    {
        Products = products ?? [];
        AllParts = allParts ?? [];

        AllParts.Add(new InHouse
        {
            PartId = 1,
            Name = "Screw",
            InStock = 50,
            Price = 0.15m,
            Min = 10,
            Max = 200,
            MachineId = 123
        });

        AllParts.Add(new Outsourced
        {
            PartId = 2,
            Name = "Bolt",
            InStock = 30,
            Price = 0.25m,
            Min = 5,
            Max = 100,
            CompanyName = "Fastenal"
        });

        Products.Add(new Product(AllParts, 1, "sean", 3, 3, 4, 6));
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