using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using Whiskey_Tycoon.UWP.ViewModels;
using Whiskey_Tycoon.lib.JSONObjects;

namespace Whiskey_Tycoon.UWP.Views
{
    public sealed partial class MainGamePage : Page
    {
        private MainGamePageViewModel viewModel => (MainGamePageViewModel) DataContext;

        public MainGamePage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (!(e.Parameter is GameObject)) {
                return;
            }
            
            DataContext = new MainGamePageViewModel((GameObject)e.Parameter);
        }

        private void btnSaveGame_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SaveGamePage), viewModel.Game);
        }
    }
}