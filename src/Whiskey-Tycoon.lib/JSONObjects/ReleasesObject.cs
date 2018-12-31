namespace Whiskey_Tycoon.lib.JSONObjects
{
    public class ReleasesObject
    {
        public string Name { get; set; }

        public uint QualityRating { get; set; }

        public float YearsAged { get; set; }

        public int ReleaseYear { get; set; }

        public int ReleaseQuarter { get; set; }

        public ulong BottlePrice { get; set; }

        public ulong BottlesSold { get; set; }

        public ulong BottlesAvailable { get; set; }

        public uint Demand { get; set; }

        public bool Archived { get; set; }

        public uint PressReviewRating { get; set; }

        public string PressReviewDescription { get; set; }
    }
}