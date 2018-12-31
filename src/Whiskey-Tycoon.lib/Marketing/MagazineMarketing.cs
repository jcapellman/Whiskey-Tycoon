using Whiskey_Tycoon.lib.Marketing.Base;

namespace Whiskey_Tycoon.lib.Marketing
{
    public class MagazineMarketing : BaseMarketing
    {
        public override string Name => "Magazine";

        public override uint Impact => 5;

        public override uint QuartersLength => 4;

        public override ulong QuarterCost => 10000;
    }
}