namespace Whiskey_Tycoon.lib.Loans.Base
{
    public abstract class BaseLoan
    {
        public abstract string Name { get; }

        public abstract ulong Amount { get; }

        public abstract uint QuartersToPayOff { get; }

        public abstract ulong QuarterlyInterest { get; }
    }
}