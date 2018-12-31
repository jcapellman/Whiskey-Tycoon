using Whiskey_Tycoon.lib.Loans.Base;

namespace Whiskey_Tycoon.lib.Loans
{
    public class LargeLoan : BaseLoan
    {
        public override string Name => "Large Loan";

        public override ulong Amount => 1000000;

        public override uint QuartersToPayOff => 24;

        public override ulong QuarterlyInterest => 125000;
    }
}