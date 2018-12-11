namespace Whiskey_Tycoon.lib.JSONObjects
{
    public class EventObject
    {
        public int Year { get; set; }

        public int Quarter { get; set; }

        public string EventText { get; set; }

        public string EventDate => $"{Year}-{Quarter}";
    }
}