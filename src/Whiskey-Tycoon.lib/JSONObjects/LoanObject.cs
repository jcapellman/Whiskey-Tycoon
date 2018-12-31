namespace Whiskey_Tycoon.lib.JSONObjects
{
    public class LoanObject
    {
        public ulong LoanedAmount { get; set; }

        public ulong LoanAmountRemaining { get; set; }

        public ulong QuarterlyInterest { get; set; }

        public ulong QuarterlyPayment { get; set; }
        
        public ulong TotalInterest { get; set; }

        public ulong QuartersRemaining { get; set; }

        public ulong PayOffAmount => LoanAmountRemaining + TotalInterest;
    }
}