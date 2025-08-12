using System;
using System.ComponentModel;
using System.Linq;

namespace InventoryApp.Models;

public class Inventory(BindingList<Product> products, BindingList<Part> allParts)
{
    public BindingList<Product> Products { get; init; } = products;
    public BindingList<Part> AllParts { get; set; } = allParts;

    // PRODUCT METHODS 
    public void AddProduct(Product product) => Products.Add(product);

    public bool RemoveProduct(int productId)
    {
        if (!Products.Any(product => product.ProductId == productId))
        {
            return false;
        }
        
        Products.Remove(Products.First(product => product.ProductId == productId));
        return true;
    }

    public Product LookupProduct(int productId)
    {
        return Products.FirstOrDefault(p => p.ProductId == productId)!;
    }

    public void UpdateProduct(int index, Product newProduct)
    {
        if (index >= 0 && index < Products.Count)
        {
            Products[index].CopyFrom(newProduct);
        }
    }
    
    // PART METHODS
    public void AddPart(Part part) => AllParts.Add(part);

    public bool DeletePart(int partId)
    {
        var partToRemove = AllParts.FirstOrDefault(p => p.PartId == partId);
        if (partToRemove == null) return false;

        AllParts.Remove(partToRemove);
        return true;
    }
    
    public Part? LookupPart(int partId) =>
        AllParts.FirstOrDefault(part => part.PartId == partId);
    
    public void UpdatePart(int index, Part newPart)
    {
        if (index >= 0 && index < AllParts.Count)
        {
            AllParts[index].CopyFrom(newPart);
        }
    }
}