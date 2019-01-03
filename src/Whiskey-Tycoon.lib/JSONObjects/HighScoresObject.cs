namespace Whiskey_Tycoon.lib.JSONObjects
{
    public class HighScoresObject
    {
        public int Ranking { get; set; }

        public string DistillerName { get; set; }

        public string DistilleryName { get; set; }

        public ulong BottlesSold { get; set; }

        public uint YearsSurvived { get; set; }

        public HighScoresObject() { }

        public HighScoresObject(HighScoresObject highScores, int ranking)
        {
            DistillerName = highScores.DistillerName;
            DistilleryName = highScores.DistilleryName;
            BottlesSold = highScores.BottlesSold;
            YearsSurvived = highScores.YearsSurvived;
            Ranking = ranking;
        }
    }
}