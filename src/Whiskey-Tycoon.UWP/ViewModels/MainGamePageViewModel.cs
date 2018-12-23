﻿using System.Collections.Generic;
using System.Linq;

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

            if (Game.BarrelsAging > 0)
            {
                eventText.Add($"{Game.BarrelsAging} barrel(s) were aging, be sure to check the Angel's share.");
            } else
            {
                eventText.Add("No barrels aged.");
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