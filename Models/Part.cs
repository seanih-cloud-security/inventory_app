public class Part
{
    public int PartId { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public int InStock { get; set; }
    public int Min { get; set; }
    public int Max { get; set; }

    public override string ToString() => $"{PartId} - {Name} - {Price:C}";

    public Part()
    {
    }

    public Part(int partId, string name, decimal price, int inStock, int min, int max)
    {
        PartId = partId;
        Name = name;
        Price = price;
        InStock = inStock;
        Min = min;
        Max = max;
    }

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