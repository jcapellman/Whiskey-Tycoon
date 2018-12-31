using Whiskey_Tycoon.lib.Loans.Base;

namespace Whiskey_Tycoon.lib.Loans
{
    public class ShortLoan : BaseLoan
    {
        public override string Name => "Short Loan";

        public override ulong Amount => 100000;

        public override uint QuartersToPayOff => 8;

        public override ulong QuarterlyInterest => 10000;
    }
}