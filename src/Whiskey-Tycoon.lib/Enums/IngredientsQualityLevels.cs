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

        public static int ToQuality(this IngredientsQualityLevels qualityLevel)
        {
            switch (qualityLevel)
            {
                case IngredientsQualityLevels.CHEAP:
                    return 0;
                case IngredientsQualityLevels.POOR:
                    return 5;
                case IngredientsQualityLevels.MID:
                    return 10;
                case IngredientsQualityLevels.HIGH:
                    return 15;
                case IngredientsQualityLevels.BEST:
                    return 20;
            }

            return 0;
        }

        public static int ToQualityQuarterIncrement(this IngredientsQualityLevels qualityLevel)
        {
            switch (qualityLevel)
            {
                case IngredientsQualityLevels.CHEAP:
                    return 1;
                case IngredientsQualityLevels.POOR:
                    return 2;
                case IngredientsQualityLevels.MID:
                    return 3;
                case IngredientsQualityLevels.HIGH:
                    return 4;
                case IngredientsQualityLevels.BEST:
                    return 5;
            }

            return 1;
        }
    }
}