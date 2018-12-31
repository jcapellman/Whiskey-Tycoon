using Whiskey_Tycoon.lib.Loans.Base;

namespace Whiskey_Tycoon.lib.Loans
{
    public class MediumLoan : BaseLoan
    {
        public override string Name => "Medium Loan";

        public override ulong Amount => 500000;

        public override uint QuartersToPayOff => 16;

        public override ulong QuarterlyInterest => 75000;
    }
}