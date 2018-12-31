using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

using Whiskey_Tycoon.lib.JSONObjects;
using Whiskey_Tycoon.UWP.ViewModels;

namespace Whiskey_Tycoon.UWP.Views
{
    public sealed partial class ReleaseManagementPage : BasePage
    {
        private ReleaseManagementPageViewModel ViewModel => (ReleaseManagementPageViewModel) DataContext;

        public ReleaseManagementPage()
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

            DataContext = new ReleaseManagementPageViewModel((GameObject)e.Parameter);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainGamePage), ViewModel.Game);
        }
    }
}