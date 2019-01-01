using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Whiskey_Tycoon.lib.Common;
using Whiskey_Tycoon.lib.Enums;
using Whiskey_Tycoon.lib.Loans.Base;
using Whiskey_Tycoon.lib.Managers;
using Whiskey_Tycoon.lib.Marketing.Base;

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

        public ObservableCollection<LoanObject> Loans { get; set; }

        public ObservableCollection<MarketingObject> Marketing { get; set; }

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
        
        private EventManager _eventManager = new EventManager();

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
            Loans = new ObservableCollection<LoanObject>();
            Marketing = new ObservableCollection<MarketingObject>();
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

        private (string reviewString, uint reviewRating) calculateReview(BatchObject batch, ulong price, uint demand)
        {
            var rating = batch.Quality;
            var descriptions = new List<string>();

            // Temporary until ML can replace
            if (batch.Quality > demand)
            {
                rating += 5;
            }

            if (batch.Quality > 80)
            {
                descriptions.Add("Good quality release");
            }
            else
            {
                descriptions.Add("Sub-par quality release");
            }

            if ((batch.Quality / Constants.QUALITY_PER_DOLLAR) > price)
            {
                descriptions.Add("Overpriced for the quality of release");

                if (rating > 20)
                {
                    rating -= 20;
                }
            }
            else
            {
                descriptions.Add("Priced properly");
                rating += 5;
            }

            if (rating > 100)
            {
                rating = 100;
            }

            return (string.Join(Environment.NewLine, descriptions), rating);
        }

        public void ReleaseBatch(BatchObject batch, ulong price, float selectedProof, ulong bottlesAvailable, uint demand, ulong bottlingCost)
        {
            var reviewMetrics = calculateReview(batch, price, demand);
            
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
                Demand = demand,
                PressReviewDescription = reviewMetrics.reviewString,
                PressReviewRating = reviewMetrics.reviewRating,
                Archived = false
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

            MoneyAvailable -= bottlingCost;
        }

        private void UpdateDemandForReleases(uint modifier)
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

            ProcessMarketing();

            AgeBarrels();

            GenerateEvents();

            GenerateSales();

            ProcessLoans();
            
            Events.Insert(0, _eventManager.GenerateEvent(CurrentYear, CurrentQuarter));
        }

        private void ProcessMarketing()
        {
            if (!Marketing.Any())
            {
                _eventManager.AddEvent("No marketing purchased");

                return;
            }

            foreach (var marketing in Marketing)
            {
                if (marketing.QuartersRemaining == 0)
                {
                    _eventManager.AddEvent($"{marketing.Name} marketing finished");

                    Marketing.Remove(marketing);

                    return;
                }

                marketing.QuartersRemaining--;

                MoneyAvailable -= marketing.QuarterlyCost;

                foreach (var release in Releases)
                {
                    release.Demand += Common.ExtensionMethods.GetRandomNumber(0, marketing.Impact);
                }

                _eventManager.AddEvent($"{marketing.Name} marketing ran, subtracted ${marketing.QuarterlyCost} from money available");
            }
        }

        public void AddLoan(BaseLoan loan)
        {
            var loanObject = new LoanObject
            {
                LoanedAmount = loan.Amount,
                QuartersRemaining = loan.QuartersToPayOff,
                QuarterlyInterest = loan.QuarterlyInterest,
                TotalInterest = 0,
                LoanAmountRemaining = loan.Amount,
                QuarterlyPayment = loan.Amount / loan.QuartersToPayOff
            };

            Loans.Add(loanObject);

            MoneyAvailable += loan.Amount;
        }

        private void ProcessLoans()
        {
            if (!Loans.Any())
            {
                _eventManager.AddEvent("No loans to process");

                return;
            }

            foreach (var loan in Loans)
            {
                if (loan.QuartersRemaining == 0)
                {
                    if (MoneyAvailable < loan.PayOffAmount)
                    {
                        _eventManager.AddEvent($"Not enough funds to process loan, game over");

                        return;
                    }

                    _eventManager.AddEvent($"Loan of ${loan.LoanedAmount} completed");

                    Loans.Remove(loan);

                    continue;
                }

                loan.TotalInterest += loan.QuarterlyInterest;
                
                loan.LoanAmountRemaining -= loan.QuarterlyPayment;

                if (MoneyAvailable < loan.QuarterlyPayment)
                {
                    _eventManager.AddEvent($"Not enough funds to process loan, game over");

                    return;
                }

                MoneyAvailable -= loan.QuarterlyPayment;

                _eventManager.AddEvent($"Loan payment of ${loan.QuarterlyPayment} applied (${loan.QuarterlyInterest} of interest added)");
            }
        }

        private void GenerateSales()
        {
            foreach (var release in Releases)
            {
                if (release.Archived)
                {
                    continue;
                }

                var perecentageSold = Common.ExtensionMethods.GetRandomNumber(0, release.Demand + 1) / 100.0;

                var bottles = (ulong)Common.ExtensionMethods.GetRandomNumber(0, (uint)(perecentageSold * release.BottlesAvailable));

                release.BottlesSold += bottles;
                release.BottlesAvailable -= bottles;

                if (release.Demand > 0)
                {
                    release.Demand--;
                }

                var moneyGenerated = release.BottlePrice * bottles;

                MoneyAvailable += moneyGenerated;
                
                _eventManager.AddEvent(bottles > 0
                    ? $"{release.Name} sold {bottles} bottle(s) and generated ${moneyGenerated}"
                    : $"{release.Name} sold no bottles");

                if (release.BottlesAvailable == 0)
                {
                    release.Archived = true;

                    _eventManager.AddEvent($"{release.Name} has sold out");
                }
            }
        }

        private void GenerateEvents()
        {
            _eventManager.AddEvent(
                BarrelsAging > 0
                    ? $"{BarrelsAging} barrel(s) were aging, be sure to check the Angel's share."
                    : "No barrels aged.");

            var randomEvent = RandomEvent.GetRandomEvent();

            switch (randomEvent)
            {
                case RandomEvents.COMPETITOR_LOST_SHIPMENT:
                    if (Releases.Any())
                    {
                        _eventManager.AddEvent("A competitor lost a shipment of bottles, demand for your products has increased");

                        UpdateDemandForReleases(Whiskey_Tycoon.lib.Common.ExtensionMethods.GetRandomNumber(Constants.EVENTS_COMPETITOR_LOST_SHIPMENT_QUALITY_DECREASES_MIN, Constants.EVENTS_COMPETITOR_LOST_SHIPMENT_QUALITY_INCREASES_MAX));
                    }
                    else
                    {
                        _eventManager.AddEvent("A competitor's quality has suffered, but you don't have any releases");
                    }
                    
                    break;
                case RandomEvents.COMPETITOR_QUALITY_ISSUES:
                    if (Releases.Any())
                    {
                        _eventManager.AddEvent("A competitor's quality has suffered, demand for your product has increased");

                        UpdateDemandForReleases(Whiskey_Tycoon.lib.Common.ExtensionMethods.GetRandomNumber(Constants.EVENTS_COMPETITOR_QUALITY_DECREASES_MIN, Constants.EVENTS_COMPETITOR_QUALITY_INCREASES_MAX));
                    }
                    else
                    {
                        _eventManager.AddEvent("A competitor's quality has suffered, but you don't have any releases");
                    }

                    break;
                case RandomEvents.HIGHER_THAN_USUAL_ANGELS_SHARE:
                    _eventManager.AddEvent("Weather has caused an unusual amount of Angel's share to be taken");

                    AgeBarrels();
                    break;
                case RandomEvents.WAREHOUSE_COLLAPSE:
                    if (Warehouses.Any())
                    {
                        var warehouse = Warehouses.GetRandomItem();

                        Warehouses.Remove(warehouse);

                        _eventManager.AddEvent($"Unfortunately Warehouse ({warehouse.Name}) along with all {warehouse.BarrelsAging} barrels aging has collapsed");
                    }

                    break;
                case RandomEvents.NOTHING:
                default:
                    _eventManager.AddEvent("Nothing out of the ordinary occurred");
                    break;
            }

            if (Warehouses.Any())
            {
                _eventManager.AddEvent(
                    $"Maintenance upkeep on {Warehouses.Count} warehouses (${WarehouseMaintenanceCost}) subtracted from account.");

                MoneyAvailable -= WarehouseMaintenanceCost;
            }
        }

        public void AddMarketing(BaseMarketing selectedMarketing)
        {
            var marketing = new MarketingObject
            {
                Name = selectedMarketing.Name,
                Impact = selectedMarketing.Impact,
                QuartersRemaining = selectedMarketing.QuartersLength,
                QuarterlyCost = selectedMarketing.QuarterCost
            };

            Marketing.Add(marketing);
        }
    }
}