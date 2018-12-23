using System;

namespace Whiskey_Tycoon.lib.Helpers
{
    public static class AngelsShare
    {
        private const int SHARE_MIN = 1;
        private const int SHARE_MAX = 2;

        public static int ComputeAngelShare(int fillAmount)
        {
            var random = new Random((int)DateTime.Now.Ticks);
            
            var value = fillAmount - random.Next(SHARE_MIN, SHARE_MAX);

            if (value < 0)
            {
                return 0;
            }

            return value;
        } 
    }
}