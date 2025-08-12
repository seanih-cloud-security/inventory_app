namespace InventoryApp.Models;

public abstract class Part(int partId, string name, decimal price, int inStock, int min, int max)
{
    public int PartId { get; set; } = partId;
    public string Name { get; set; } = name;
    public decimal Price { get; set; } = price;
    public int InStock { get; set; } = inStock;
    public int Min { get; set; } = min;
    public int Max { get; set; } = max;
    
    public void CopyFrom(Part other)
    {
        PartId = other.PartId;
        Name = other.Name;
        Price = other.Price;
        InStock = other.InStock;
        Min = other.Min;
        Max = other.Max;
    }
}