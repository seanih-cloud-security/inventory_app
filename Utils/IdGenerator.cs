using System.Linq;
using InventoryApp.Models;

namespace InventoryApp.Utils
{
    public static class IdGenerator
    {
        private static int _currentPartId = AppData.AppInventory.AllParts.Count;
        private static int _currentProductId = AppData.AppInventory.Products.Count;

        public static int GeneratePartId()
        {
            return ++_currentPartId;
        }

        public static int GenerateProductID()
        {
            return ++_currentProductId;
        }
    }
}