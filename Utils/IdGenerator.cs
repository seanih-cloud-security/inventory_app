namespace InventoryApp.Utils
{
    public static class IDGenerator
    {
        private static int _currentPartId = 0;
        private static int _currentProductId = 0;

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