using System.Collections.Generic;

namespace Whiskey_Tycoon.lib.JSONObjects
{
    public class WarehouseObject
    {
        public string Name { get; set; }

        public List<BarrelObject> Barrels { get; set; }

        public void AgeBarrels()
        {
            foreach (var barrel in Barrels)
            {
                barrel.AgeBarrel();
            }
        }

        public WarehouseObject()
        {
            Barrels = new List<BarrelObject>();
        }
    }
}