using System.Collections.Generic;
using System.Linq;

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

        public int Quality { get; set; }

        public List<BarrelObject> Barrels { get; set; }

        public int BarrelFillAmount => Barrels.FirstOrDefault().FillAmount;

        public float BarrelAgeInYears => BarrelQuarterAge / 4.0f;

        public BatchObject()
        {
            Barrels = new List<BarrelObject>();
        }

        public void AgeBatch()
        {
            BarrelQuarterAge++;

            Quality += QualityLevel.ToQualityQuarterIncrement();

            foreach (var barrel in Barrels)
            {
                barrel.AgeBarrel();
            }
        }
    }
}