namespace Whiskey_Tycoon.lib.JSONObjects
{
    public class BarrelObject
    {
        public int ID { get; set; }

        public int BarrelYear { get; set; }

        public int BarrelQuarter { get; set; }

        public int Quarters { get; set; }

        public int FillAmount { get; set; }

        public void AgeBarrel()
        {
            FillAmount = (int) (100 - (Quarters * .25));
        }
    }
}