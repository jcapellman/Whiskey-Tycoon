using System.Collections.Generic;
using System.Linq;
using Whiskey_Tycoon.lib.Common;
using Whiskey_Tycoon.lib.Enums;
using Whiskey_Tycoon.lib.JSONObjects;
using ExtensionMethods = Whiskey_Tycoon.lib.Common.ExtensionMethods;

namespace Whiskey_Tycoon.UWP.ViewModels
{
    public class MainGamePageViewModel : BaseViewModel
    {
        public MainGamePageViewModel(GameObject gameObject)
        {
            Game = gameObject;
        }

        public void NextQuarter()
        {
            if (Game.CurrentQuarter == 4)
            {
                Game.CurrentYear++;
                Game.CurrentQuarter = 1;
            }
            else
            {
                Game.CurrentQuarter++;
            }

            Game.AgeBarrels();

            var eventText = new List<string>
            {
                Game.BarrelsAging > 0
                    ? $"{Game.BarrelsAging} barrel(s) were aging, be sure to check the Angel's share."
                    : "No barrels aged."
            };

            var randomEvent = RandomEvent.GetRandomEvent();

            switch (randomEvent)
            {
                case RandomEvents.COMPETITOR_LOST_SHIPMENT:
                    if (Game.Releases.Any())
                    {
                        eventText.Add("A competitor lost a shipment of bottles, demand for your products has increased");

                        Game.UpdateDemandForReleases(ExtensionMethods.GetRandomNumber(Constants.EVENTS_COMPETITOR_LOST_SHIPMENT_QUALITY_DECREASES_MIN, Constants.EVENTS_COMPETITOR_LOST_SHIPMENT_QUALITY_INCREASES_MAX));
                    }
                    else
                    {
                        eventText.Add("A competitor's quality has suffered, but you don't have any releases");
                    }

                    eventText.Add("");
                    break;
                case RandomEvents.COMPETITOR_QUALITY_ISSUES:
                    if (Game.Releases.Any())
                    {
                        eventText.Add("A competitor's quality has suffered, demand for your product has increased");

                        Game.UpdateDemandForReleases(ExtensionMethods.GetRandomNumber(Constants.EVENTS_COMPETITOR_QUALITY_DECREASES_MIN, Constants.EVENTS_COMPETITOR_QUALITY_INCREASES_MAX));
                    }
                    else
                    {
                        eventText.Add("A competitor's quality has suffered, but you don't have any releases");
                    }

                    break;
                case RandomEvents.HIGHER_THAN_USUAL_ANGELS_SHARE:
                    eventText.Add("Weather has caused an unusual amount of Angel's share to be taken");

                    Game.AgeBarrels();
                    break;
                case RandomEvents.WAREHOUSE_COLLAPSE:
                    if (Game.Warehouses.Any())
                    {
                        var warehouse = Game.Warehouses.GetRandomItem();

                        Game.Warehouses.Remove(warehouse);

                        eventText.Add($"Unfortunately Warehouse ({warehouse.Name}) along with all {warehouse.BarrelsAging} barrels aging has collapsed");
                    }

                    break;
                case RandomEvents.NOTHING:
                default:
                    eventText.Add("Nothing out of the ordinary occurred");
                    break;
            }

            if (Game.Warehouses.Any())
            {
                eventText.Add(
                    $"Maintenance upkeep on {Game.Warehouses.Count} warehouses (${Game.WarehouseMaintenanceCost}) subtracted from account.");

                Game.MoneyAvailable -= Game.WarehouseMaintenanceCost;
            }
            
            Game.Events.Insert(0, new EventObject
            {
                EventText = string.Join(System.Environment.NewLine, eventText),
                Year = Game.CurrentYear,
                Quarter = Game.CurrentQuarter
            });

            Game = Game;
        }
    }
}