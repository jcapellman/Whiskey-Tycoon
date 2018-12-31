using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Whiskey_Tycoon.lib.Common
{
    public static class ExtensionMethods
    {
        public static uint GetRandomNumber(uint min, uint max)
        {
            var random = new Random((int)DateTime.Now.Ticks);

            return (uint)random.Next((int)min, (int)max);
        }

        public static T GetRandomItem<T>(this ObservableCollection<T> container)
        {
            if (!container.Any())
            {
                return default;
            }

            var random = new Random((int)DateTime.Now.Ticks);

            var index = random.Next(0, container.Count - 1);

            return container[index];
        }
    }
}