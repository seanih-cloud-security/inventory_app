namespace InventoryApp.Models;

public class Outsourced(string companyName, int partId, string name, decimal price, int inStock, int min, int max) :  Part(partId, name, price, inStock, min, max)
{
    public string CompanyName { get; set; } = companyName;
}