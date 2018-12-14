using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Whiskey_Tycoon.lib.Enums;

namespace Whiskey_Tycoon.lib.JSONObjects
{
    public class GameObject
    {
        public int ID { get; set; }

        public string DistilleryName { get; set; }

        public string DistillerName { get; set; }

        public string CurrentDate => $"{CurrentYear}-{CurrentQuarter}";

        public int CurrentYear { get; set; } = DateTime.Now.Year;

        public int CurrentQuarter { get; set; } = 1;
        
        public ulong MoneyAvailable { get; set; } = 1000000;

        public DateTime SaveDate { get; set; }

        public string FileName { get; set; }

        public string SaveDisplayName { get; set; }
            
        public List<WarehouseObject> Warehouses { get; set; }

        public ObservableCollection<EventObject> Events { get; set; }

        public int BarrelsAging => Warehouses.Sum(a => a.Barrels.Count);

        public ulong WarehouseMaintenanceCost
        {
            get
            {
                ulong total = 0;
                
                foreach (var warehouse in Warehouses)
                {
                    total += warehouse.Size.ToQuarterCost();
                }

                return total;
            }
        }

        public GameObject()
        {
            Warehouses = new List<WarehouseObject>();
            Events = new ObservableCollection<EventObject>();
        }

        public void AddWarehouse(string warehouseName, string selectedWarehouseSize, ulong warehouseCost)
        {
            MoneyAvailable -= warehouseCost;

            var warehouseObject = new WarehouseObject
            {
                Barrels = new List<BarrelObject>(),
                Name = warehouseName,
                Size = (WarehouseSizes) Enum.Parse(typeof(WarehouseSizes), selectedWarehouseSize)
            };

            Warehouses.Add(warehouseObject);
        }
    }
}