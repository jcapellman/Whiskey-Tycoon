using System;
using System.Collections.Generic;

using Whiskey_Tycoon.lib.JSONObjects;

namespace Whiskey_Tycoon.lib.Managers
{
    public class EventManager
    {
        private readonly List<string> _events = new List<string>();

        public void AddEvent(string eventString) => _events.Add(eventString);

        public EventObject GenerateEvent(int currentYear, int currentQuarter)
        {
            var eventObject = new EventObject
            {
                EventText = string.Join(Environment.NewLine, _events),
                Year = currentYear,
                Quarter = currentQuarter
            };

            _events.Clear();

            return eventObject;
        }
    }
}