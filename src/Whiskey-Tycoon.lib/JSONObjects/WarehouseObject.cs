using System;
using System.Collections.Generic;

using Whiskey_Tycoon.lib.Enums;

namespace Whiskey_Tycoon.lib.JSONObjects
{
    public class WarehouseObject
    {
        public Guid ID { get; set; }

        public string Name { get; set; }

        public List<BarrelObject> Barrels { get; set; }

        public WarehouseSizes Size { get; set; }

        public int SpaceAvailable => (int) Size - Barrels.Count;

        public void AgeBarrels()
        {
            foreach (var barrel in Barrels)
            {
                barrel.AgeBarrel();
            }
        }

        public WarehouseObject()
        {
            ID = Guid.NewGuid();

            Barrels = new List<BarrelObject>();
        }
    }
}