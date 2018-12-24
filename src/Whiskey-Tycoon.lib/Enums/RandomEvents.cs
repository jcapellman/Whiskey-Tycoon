using System;
using System.Collections.Generic;
using System.Linq;

namespace Whiskey_Tycoon.lib.Enums
{
    public enum RandomEvents
    {
        NOTHING = 87,
        WAREHOUSE_COLLAPSE = 1,
        COMPETITOR_QUALITY_ISSUES = 5,
        HIGHER_THAN_USUAL_ANGELS_SHARE = 4,
        COMPETITOR_LOST_SHIPMENT = 3
    }

    public static class RandomEvent
    {
        private static readonly List<RandomEvents> EventsList = new List<RandomEvents>();

        public static RandomEvents GetRandomEvent()
        {
            if (!EventsList.Any())
            {
                foreach (var name in Enum.GetNames(typeof(RandomEvents)))
                {
                    var enumName = (RandomEvents) Enum.Parse(typeof(RandomEvents), name);
                    
                    for (var x = 0; x < (int)enumName; x++)
                    {
                        EventsList.Add(enumName);
                    }
                }
            }

            var random = new Random((int) DateTime.Now.Ticks);

            var index = random.Next(0, 99);

            return EventsList[index];
        }
    }
}