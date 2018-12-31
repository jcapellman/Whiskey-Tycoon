namespace Whiskey_Tycoon.lib.Common
{
    public static class Constants
    {
        public const string FILENAME_OPTIONS = "options.json";

        public const string FILE_SAVEGAME_EXTENSION = "wtsg";

        public const string FILE_SAVEGAME_DEFAULT_NAME = "New Save Game";

        public const int NUMBER_BOTTLES_PER_BARREL = 265;

        public const double BARREL_SIZE_ML = 200626.824;

        public const int DEFAULT_BARREL_PROOF = 130;

        public const int BOTTLE_SIZE = 750;

        public const int MINIMUM_PROOF = 80;

        public const ulong BOTTLE_COST = 5;

        public const uint QUALITY_PER_DOLLAR = 2;

        // Events
        public const uint EVENTS_COMPETITOR_QUALITY_DECREASES_MIN = 1;
        public const uint EVENTS_COMPETITOR_QUALITY_INCREASES_MAX = 3;

        public const uint EVENTS_COMPETITOR_LOST_SHIPMENT_QUALITY_DECREASES_MIN = 1;
        public const uint EVENTS_COMPETITOR_LOST_SHIPMENT_QUALITY_INCREASES_MAX = 3;
    }
}