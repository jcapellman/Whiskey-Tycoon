using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

using Whiskey_Tycoon.lib.Marketing.Base;

namespace Whiskey_Tycoon.lib.Managers
{
    public class MarketingManager
    {
        public static ObservableCollection<BaseMarketing> GetMarketingTypes() =>
            new ObservableCollection<BaseMarketing>(Assembly.GetAssembly(typeof(BaseMarketing)).GetTypes()
                .Where(a => a.BaseType == typeof(BaseMarketing) && !a.IsAbstract)
                .Select(b => (BaseMarketing)Activator.CreateInstance(b)).OrderBy(a => a.Impact));
    }
}