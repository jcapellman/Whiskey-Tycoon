using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Whiskey_Tycoon.lib.Common;
using Whiskey_Tycoon.lib.Enums;
using ExtensionMethods = Whiskey_Tycoon.lib.Enums.ExtensionMethods;

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
            
        public ObservableCollection<WarehouseObject> Warehouses { get; set; }

        public ObservableCollection<EventObject> Events { get; set; }

        public int BarrelsAging => Warehouses.Sum(a => a.BarrelsAging);

        public ObservableCollection<ReleasesObject> Releases { get; set; }

        public ulong BottlesSold
        {
            get
            {
                ulong total = 0;

                foreach (var release in Releases)
                {
                    total += release.BottlesSold;
                }

                return total;
            }
        }

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

        public void AgeBarrels()
        {
            foreach (var warehouse in Warehouses)
            {
                warehouse.AgeBatches();
            }
        }

        public GameObject()
        {
            Warehouses = new ObservableCollection<WarehouseObject>();
            Events = new ObservableCollection<EventObject>();
            Releases = new ObservableCollection<ReleasesObject>();
        }

        public void UpdateWarehouse(WarehouseObject warehouseObject)
        {
            for (var x = 0; x < Warehouses.Count; x++)
            {
                if (Warehouses[x].ID != warehouseObject.ID)
                {
                    continue;
                }

                Warehouses[x] = warehouseObject;

                return;
            }
        }

        public void AddWarehouse(string warehouseName, string selectedWarehouseSize, ulong warehouseCost)
        {
            MoneyAvailable -= warehouseCost;

            var warehouseObject = new WarehouseObject
            {
                Batches = new ObservableCollection<BatchObject>(),
                Name = warehouseName,
                Size = (WarehouseSizes) Enum.Parse(typeof(WarehouseSizes), selectedWarehouseSize)
            };

            Warehouses.Add(warehouseObject);
        }

        public void ReleaseBatch(BatchObject batch, ulong price, float selectedProof, ulong bottlesAvailable, int demand)
        {
            var releaseObject = new ReleasesObject
            {
                Name = batch.Name,
                BottlesSold = 0,
                BottlePrice = price,
                QualityRating = batch.Quality,
                ReleaseQuarter = CurrentQuarter,
                ReleaseYear = CurrentYear,
                YearsAged = batch.BarrelQuarterAge / 4.0f,
                BottlesAvailable = bottlesAvailable,
                Demand = demand
            };

            Releases.Add(releaseObject);

            foreach (var warehouse in Warehouses)
            {
                var found = warehouse.Batches.Any(a => a.ID == batch.ID);

                if (!found)
                {
                    continue;
                }

                warehouse.RemoveBatch(batch);
            }
        }

        private void UpdateDemandForReleases(int modifier)
        {
            foreach (var release in Releases)
            {
                release.Demand += modifier;
            }
        }

        public void NextQuarter()
        {
            if (CurrentQuarter == 4)
            {
                CurrentYear++;
                CurrentQuarter = 1;
            }
            else
            {
                CurrentQuarter++;
            }

            AgeBarrels();

            GenerateEvents();

            GenerateSales();
        }

        private void GenerateSales()
        {
            foreach (var release in Releases)
            {
                var perecentageSold = Common.ExtensionMethods.GetRandomNumber(0, release.Demand + 1) / 100.0;

                var bottles = (ulong)Common.ExtensionMethods.GetRandomNumber(0, (int)(perecentageSold * release.BottlesAvailable));

                release.BottlesSold += bottles;
                release.BottlesAvailable -= bottles;
                release.Demand--;

                MoneyAvailable += release.BottlePrice * bottles;

                release.Demand = Math.Abs(release.Demand);
            }
        }

        private void GenerateEvents()
        {
            var eventText = new List<string>
            {
                BarrelsAging > 0
                    ? $"{BarrelsAging} barrel(s) were aging, be sure to check the Angel's share."
                    : "No barrels aged."
            };

            var randomEvent = RandomEvent.GetRandomEvent();

            switch (randomEvent)
            {
                case RandomEvents.COMPETITOR_LOST_SHIPMENT:
                    if (Releases.Any())
                    {
                        eventText.Add("A competitor lost a shipment of bottles, demand for your products has increased");

                        UpdateDemandForReleases(Whiskey_Tycoon.lib.Common.ExtensionMethods.GetRandomNumber(Constants.EVENTS_COMPETITOR_LOST_SHIPMENT_QUALITY_DECREASES_MIN, Constants.EVENTS_COMPETITOR_LOST_SHIPMENT_QUALITY_INCREASES_MAX));
                    }
                    else
                    {
                        eventText.Add("A competitor's quality has suffered, but you don't have any releases");
                    }

                    eventText.Add("");
                    break;
                case RandomEvents.COMPETITOR_QUALITY_ISSUES:
                    if (Releases.Any())
                    {
                        eventText.Add("A competitor's quality has suffered, demand for your product has increased");

                        UpdateDemandForReleases(Whiskey_Tycoon.lib.Common.ExtensionMethods.GetRandomNumber(Constants.EVENTS_COMPETITOR_QUALITY_DECREASES_MIN, Constants.EVENTS_COMPETITOR_QUALITY_INCREASES_MAX));
                    }
                    else
                    {
                        eventText.Add("A competitor's quality has suffered, but you don't have any releases");
                    }

                    break;
                case RandomEvents.HIGHER_THAN_USUAL_ANGELS_SHARE:
                    eventText.Add("Weather has caused an unusual amount of Angel's share to be taken");

                    AgeBarrels();
                    break;
                case RandomEvents.WAREHOUSE_COLLAPSE:
                    if (Warehouses.Any())
                    {
                        var warehouse = Warehouses.GetRandomItem();

                        Warehouses.Remove(warehouse);

                        eventText.Add($"Unfortunately Warehouse ({warehouse.Name}) along with all {warehouse.BarrelsAging} barrels aging has collapsed");
                    }

                    break;
                case RandomEvents.NOTHING:
                default:
                    eventText.Add("Nothing out of the ordinary occurred");
                    break;
            }

            if (Warehouses.Any())
            {
                eventText.Add(
                    $"Maintenance upkeep on {Warehouses.Count} warehouses (${WarehouseMaintenanceCost}) subtracted from account.");

                MoneyAvailable -= WarehouseMaintenanceCost;
            }

            Events.Insert(0, new EventObject
            {
                EventText = string.Join(System.Environment.NewLine, eventText),
                Year = CurrentYear,
                Quarter = CurrentQuarter
            });
        }
    }
}