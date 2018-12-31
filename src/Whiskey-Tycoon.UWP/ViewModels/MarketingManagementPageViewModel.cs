using System.Collections.ObjectModel;
using System.Linq;

using Whiskey_Tycoon.lib.JSONObjects;
using Whiskey_Tycoon.lib.Loans.Base;
using Whiskey_Tycoon.lib.Managers;

namespace Whiskey_Tycoon.UWP.ViewModels
{
    public class MarketingManagementPageViewModel : BaseViewModel
    {
        private ObservableCollection<BaseLoan> _marketingTypes;

        public ObservableCollection<BaseLoan> MarketingTypes
        {
            get => _marketingTypes;

            set
            {
                _marketingTypes = value;

                OnPropertyChanged();
            }
        }

        private bool _enableBuyMarketingButton;

        public bool EnableBuyMarketingButton
        {
            get => _enableBuyMarketingButton;

            set
            {
                _enableBuyMarketingButton = value;

                OnPropertyChanged();
            }
        }

        private BaseLoan _selectedMarketing;

        public BaseLoan SelectedMarketing
        {
            get => _selectedMarketing;

            set
            {
                _selectedMarketing = value;

                OnPropertyChanged();
            }
        }

        public MarketingManagementPageViewModel(GameObject game)
        {
            Game = game;

            MarketingTypes = LoanManager.GetLoanTypes();

            SelectedMarketing = MarketingTypes.FirstOrDefault();

            EnableBuyMarketingButton = !Game.Loans.Any();
        }

        public void AddMarketing()
        {
            Game.AddMarketing(SelectedMarketing);
        }
    }
}