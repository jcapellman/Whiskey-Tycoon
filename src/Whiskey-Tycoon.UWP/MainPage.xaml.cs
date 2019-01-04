using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using Whiskey_Tycoon.UWP.ViewModels;
using Whiskey_Tycoon.UWP.Views;

namespace Whiskey_Tycoon.UWP
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();

            DataContext = new MainPageViewModel();
        }

        private void btnNewGame_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(NewGamePage));
        }

        private void btnLoadGame_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LoadGamePage));
        }

        private void btnOptions_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(OptionsPage));
        }

        private void btnHighScores_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(HighScorePage));
        }
    }
}