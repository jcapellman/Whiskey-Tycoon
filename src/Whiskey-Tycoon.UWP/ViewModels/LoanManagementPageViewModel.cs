using Whiskey_Tycoon.lib.JSONObjects;

namespace Whiskey_Tycoon.UWP.ViewModels
{
    public class LoanManagementPageViewModel : BaseViewModel
    {
        public LoanManagementPageViewModel(GameObject game)
        {
            Game = game;
        }

        public void AddLoan(ulong amount, ulong quarterlyInterest, uint quarterInstallments)
        {
            Game.AddLoan(amount, quarterlyInterest, quarterInstallments);
        }
    }
}