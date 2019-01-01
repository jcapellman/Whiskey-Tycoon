using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

using Whiskey_Tycoon.lib.JSONObjects;
using Whiskey_Tycoon.UWP.ViewModels;

namespace Whiskey_Tycoon.UWP.Views
{
    public sealed partial class MarketingManagementPage : BasePage
    {
        private MarketingManagementPageViewModel ViewModel => (MarketingManagementPageViewModel)DataContext;

        public MarketingManagementPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (!(e.Parameter is GameObject))
            {
                return;
            }

            DataContext = new MarketingManagementPageViewModel((GameObject)e.Parameter);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainGamePage), ViewModel.Game);
        }

        private void btnBuyMarketing_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.AddMarketing();

            ShowMessage($"{ViewModel.SelectedMarketing.Name} marketing obtained!");

            Frame.Navigate(typeof(MainGamePage), ViewModel.Game);
        }
    }
}