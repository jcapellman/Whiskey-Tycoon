using System.Collections.ObjectModel;
using System.Linq;

using Whiskey_Tycoon.lib.JSONObjects;
using Whiskey_Tycoon.lib.Loans.Base;
using Whiskey_Tycoon.lib.Managers;

namespace Whiskey_Tycoon.UWP.ViewModels
{
    public class LoanManagementPageViewModel : BaseViewModel
    {
        private ObservableCollection<BaseLoan> _loanTypes;

        public ObservableCollection<BaseLoan> LoanTypes
        {
            get => _loanTypes;

            set
            {
                _loanTypes = value;

                OnPropertyChanged();
            }
        }

        private BaseLoan _selectedLoan;

        public BaseLoan SelectedLoan
        {
            get => _selectedLoan;

            set
            {
                _selectedLoan = value;

                OnPropertyChanged();
            }
        }

        public LoanManagementPageViewModel(GameObject game)
        {
            Game = game;

            LoanTypes = LoanManager.GetLoanTypes();

            SelectedLoan = LoanTypes.FirstOrDefault();
        }

        public void AddLoan()
        {
            Game.AddLoan(SelectedLoan);
        }
    }
}