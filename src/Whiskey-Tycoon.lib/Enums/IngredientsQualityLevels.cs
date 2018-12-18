namespace Whiskey_Tycoon.lib.Enums
{
    public enum IngredientsQualityLevels
    {
        CHEAP = 100,
        POOR = 110,
        MID = 120,
        HIGH = 130,
        BEST = 140
    }

    public static class IngredientsExtensions
    {
        public static float ToQualityLevelMultiplier(this IngredientsQualityLevels qualityLevel) => ((int)qualityLevel / 100.0f);
    }
}