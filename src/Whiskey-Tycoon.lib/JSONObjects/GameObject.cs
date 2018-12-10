using System;

namespace Whiskey_Tycoon.lib.JSONObjects
{
    public class GameObject
    {
        public string DistilleryName { get; set; }

        public string DistillerName { get; set; }

        public string CurrentDate => $"{CurrentYear}-{CurrentQuarter}";

        public int CurrentYear { get; set; } = DateTime.Now.Year;

        public int CurrentQuarter { get; set; } = 1;

        // In thousands
        public int MoneyAvailable { get; set; } = 1000;

        public DateTime SaveDate { get; set; }

        public string FileName { get; set; }

        public string SaveDisplayName { get; set; }
    }
}