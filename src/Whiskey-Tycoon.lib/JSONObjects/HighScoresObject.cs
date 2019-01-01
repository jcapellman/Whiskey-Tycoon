namespace Whiskey_Tycoon.lib.JSONObjects
{
    public class HighScoresObject
    {
        public string DistillerName { get; set; }

        public string DistilleryName { get; set; }

        public ulong BottlesSold { get; set; }

        public uint YearsSurvived { get; set; }
    }
}