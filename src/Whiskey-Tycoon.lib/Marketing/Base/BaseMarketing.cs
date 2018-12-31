namespace Whiskey_Tycoon.lib.Marketing.Base
{
    public abstract class BaseMarketing
    {
        public abstract string Name { get; }

        public abstract uint Impact { get; }

        public abstract uint QuartersLength { get; }

        public abstract ulong QuarterCost { get; }
    }
}