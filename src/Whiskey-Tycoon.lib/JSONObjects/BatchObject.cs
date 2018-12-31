using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Whiskey_Tycoon.lib.Enums;

namespace Whiskey_Tycoon.lib.JSONObjects
{
    public class BatchObject
    {
        public Guid ID { get; set; }

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

        public int Demand { get; set; }

        public ObservableCollection<PressSampleReviewObject> PressSampleReviews { get; set; }

        public BatchObject()
        {
            ID = Guid.NewGuid();
            Barrels = new List<BarrelObject>();
            PressSampleReviews = new ObservableCollection<PressSampleReviewObject>();
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

        public void ReleaseToPress(int currentYear, int currentQuarter)
        {
            var pressPreview = new PressSampleReviewObject();

            var review = new List<string>();

            // Temporary logic until ML can replace this
            if (Quality < 70)
            {
                Demand -= Quality / 10;
                pressPreview.Positive = false;
                review.Add("Could use more aging or priced cheaply.");
            } else if (Quality > 70)
            {
                Demand += Quality / 10;
                pressPreview.Positive = true;
                review.Add("Good amount of aging.");
            }

            Demand = Math.Abs(Demand);

            review.Add(QualityLevel < IngredientsQualityLevels.MID
                ? "Better quality ingredients would be wise, hopefully it is priced cheap."
                : "Smart choice to utilize better quality ingredients.");

            pressPreview.Review = string.Join(System.Environment.NewLine, review);
            pressPreview.Quarter = currentQuarter;
            pressPreview.Year = currentYear;

            PressSampleReviews.Insert(0, pressPreview);
        }
    }
}