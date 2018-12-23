using Whiskey_Tycoon.lib.Helpers;

namespace Whiskey_Tycoon.lib.JSONObjects
{
    public class BarrelObject
    {
        public int ID { get; set; }
        
        public int FillAmount { get; set; }

        public BarrelObject()
        {
            FillAmount = 100;
        }

        public void AgeBarrel()
        {
            FillAmount = AngelsShare.ComputeAngelShare(FillAmount);
        }
    }
}