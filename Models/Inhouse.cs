namespace InventoryApp.Models;

public class Inhouse(int machineId, int partId, string name, decimal price, int inStock, int min, int max) : Part(partId, name, price, inStock, min, max)
{
    public int MachineId { get; set; } = machineId;
}