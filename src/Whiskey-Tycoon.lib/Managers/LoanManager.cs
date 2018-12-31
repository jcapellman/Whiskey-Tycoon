using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

using Whiskey_Tycoon.lib.Loans.Base;

namespace Whiskey_Tycoon.lib.Managers
{
    public class LoanManager
    {
        public static ObservableCollection<BaseLoan> GetLoanTypes() =>
            new ObservableCollection<BaseLoan>(Assembly.GetAssembly(typeof(BaseLoan)).GetTypes()
                .Where(a => a.BaseType == typeof(BaseLoan) && !a.IsAbstract)
                .Select(b => (BaseLoan) Activator.CreateInstance(b)).OrderBy(a => a.Amount));
    }
}