using System.Collections.ObjectModel;
using System.Linq;

using Whiskey_Tycoon.lib.JSONObjects;
using Whiskey_Tycoon.lib.Managers;
using Whiskey_Tycoon.lib.Marketing.Base;

namespace Whiskey_Tycoon.UWP.ViewModels
{
    public class MarketingManagementPageViewModel : BaseViewModel
    {
        private ObservableCollection<BaseMarketing> _marketingTypes;

        public ObservableCollection<BaseMarketing> MarketingTypes
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

        private BaseMarketing _selectedMarketing;

        public BaseMarketing SelectedMarketing
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

            MarketingTypes = MarketingManager.GetMarketingTypes();

            SelectedMarketing = MarketingTypes.FirstOrDefault();

            EnableBuyMarketingButton = !Game.Marketing.Any();
        }

        public void AddMarketing()
        {
            Game.AddMarketing(SelectedMarketing);
        }
    }
}