﻿namespace Whiskey_Tycoon.lib.JSONObjects
{
    public class GameObject
    {
        public string DistilleryName { get; set; }

        public string DistillerName { get; set; }

        // In thousands
        public int MoneyAvailable { get; set; } = 1000;
    }
}