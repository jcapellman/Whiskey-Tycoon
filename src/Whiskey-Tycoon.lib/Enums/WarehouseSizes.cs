namespace Whiskey_Tycoon.lib.Enums
{
    public enum WarehouseSizes
    {
        Tiny = 100,
        Small = 1000,
        Medium = 5000,
        Large = 10000,
        ExtraLarge = 50000
    }

    public static class ExtensionMethods
    {
        public static int ToCost(this WarehouseSizes warehouseSize)
        {
            switch (warehouseSize)
            {
                case WarehouseSizes.Tiny:
                    return 10000;
                case WarehouseSizes.Small:
                    return 20000;
                case WarehouseSizes.Medium:
                    return 50000;
                case WarehouseSizes.Large:
                    return 100000;
                case WarehouseSizes.ExtraLarge:
                    return 500000;
            }

            return -1;
        }
    }
}