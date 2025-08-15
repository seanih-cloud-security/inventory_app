using System;
using System.ComponentModel;
using System.Linq;

namespace InventoryApp.Models;

public class Inventory
{
    public BindingList<Product> Products { get; init; }
    public static BindingList<Part>? AllParts { get; set; }

    public Inventory(BindingList<Product> products, BindingList<Part> allParts)
    {
        Products = products;
        AllParts = allParts;
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