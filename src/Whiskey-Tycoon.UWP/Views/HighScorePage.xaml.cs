using Windows.UI.Xaml;

using Whiskey_Tycoon.UWP.ViewModels;

namespace Whiskey_Tycoon.UWP.Views
{
    public sealed partial class HighScorePage : BasePage
    {
        private HighScorePageViewModel ViewModel => (HighScorePageViewModel) DataContext;

        public HighScorePage()
        {
            InitializeComponent();

            DataContext = new HighScorePageViewModel();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private async void btnClearHighScores_Click(object sender, RoutedEventArgs e)
        {
            var result = await ShowYesNoDialogAsync("Are you sure you want to clear the high scores?");

            if (!result)
            {
                return;
            }

            ViewModel.ClearHighScores();
        }
    }
}