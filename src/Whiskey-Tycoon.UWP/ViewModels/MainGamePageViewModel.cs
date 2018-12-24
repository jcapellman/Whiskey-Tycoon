﻿using System.Collections.Generic;
using System.Linq;
using Whiskey_Tycoon.lib.Enums;
using Whiskey_Tycoon.lib.JSONObjects;

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

            foreach (var warehouse in Game.Warehouses)
            {
                warehouse.AgeBatches();
            }

            var eventText = new List<string>();

            eventText.Add(Game.BarrelsAging > 0
                ? $"{Game.BarrelsAging} barrel(s) were aging, be sure to check the Angel's share."
                : "No barrels aged.");

            if (Game.Warehouses.Any())
            {
                eventText.Add(
                    $"Maintenance upkeep on {Game.Warehouses.Count} warehouses (${Game.WarehouseMaintenanceCost}) subtracted from account.");

                Game.MoneyAvailable -= Game.WarehouseMaintenanceCost;
            }

            var randomEvent = RandomEvent.GetRandomEvent();
            
            switch (randomEvent)
            {
                case RandomEvents.COMPETITOR_LOST_SHIPMENT:
                    eventText.Add("A competitor lost a shipment of bottles, demand for your products has increased");
                    break;
                case RandomEvents.COMPETITOR_QUALITY_ISSUES:
                    eventText.Add("A competitor's quality has suffered, demand for your product has increased");
                    break;
                case RandomEvents.HIGHER_THAN_USUAL_ANGELS_SHARE:
                    eventText.Add("Weather has caused an unusual amount of Angel's share to be taken");
                    break;
                case RandomEvents.WAREHOUSE_COLLAPSE:
                    eventText.Add("Unfortunately a warehouse has collapsed");
                    break;
                case RandomEvents.NOTHING:
                default:
                    eventText.Add("Nothing out of the ordinary occurred");
                    break;
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