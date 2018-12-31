using Whiskey_Tycoon.lib.Marketing.Base;

namespace Whiskey_Tycoon.lib.Marketing
{
    public class TVMarketing : BaseMarketing
    {
        public override string Name => "TV";

        public override uint Impact => 10;

        public override uint QuartersLength => 4;

        public override ulong QuarterCost => 30000;
    }
}