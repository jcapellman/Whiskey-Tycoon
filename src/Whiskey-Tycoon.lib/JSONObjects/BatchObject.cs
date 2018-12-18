using System.Collections.Generic;

using Whiskey_Tycoon.lib.Enums;

namespace Whiskey_Tycoon.lib.JSONObjects
{
    public class BatchObject
    {
        public string Name { get; set; }

        public int BarrelQuarter { get; set; }

        public int BarrelYear { get; set; }

        public int BarrelQuarterAge { get; set; }

        public string BarrelingDate => $"{BarrelQuarter}-{BarrelYear}";

        public int NumberBarrels => Barrels.Count;

        public IngredientsQualityLevels QualityLevel { get; set; }

        public BatchTypes BatchType { get; set; }

        public List<BarrelObject> Barrels { get; set; }

        public BatchObject()
        {
            Barrels = new List<BarrelObject>();
        }
    }
}