namespace Whiskey_Tycoon.lib.JSONObjects
{
    public class ReleasesObject
    {
        public string Name { get; set; }

        public int QualityRating { get; set; }

        public float YearsAged { get; set; }

        public int ReleaseYear { get; set; }

        public int ReleaseQuarter { get; set; }

        public float BottlePrice { get; set; }

        public ulong BottlesSold { get; set; }

        public ulong BottlesAvailable { get; set; }

        public int Demand { get; set; }
    }
}