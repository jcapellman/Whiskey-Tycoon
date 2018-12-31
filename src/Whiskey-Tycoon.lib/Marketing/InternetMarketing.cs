using Whiskey_Tycoon.lib.Marketing.Base;

namespace Whiskey_Tycoon.lib.Marketing
{
    public class InternetMarketing : BaseMarketing
    {
        public override string Name => "Internet";

        public override uint Impact => 15;

        public override uint QuartersLength => 4;

        public override ulong QuarterCost => 50000;
    }
}