namespace InventoryApp.Models;

public class Outsourced(int partId, string name, decimal price, int inStock, int min, int max, string companyName) :  Part(partId, name, price, inStock, min, max)
{
    public string CompanyName { get; set; } = companyName;
}