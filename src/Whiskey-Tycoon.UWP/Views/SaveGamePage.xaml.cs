using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using Whiskey_Tycoon.lib.JSONObjects;

using Whiskey_Tycoon.UWP.ViewModels;

namespace Whiskey_Tycoon.UWP.Views
{
    public sealed partial class SaveGamePage : BasePage
    {
        private SaveGamePageViewModel viewModel => (SaveGamePageViewModel) DataContext;

        public SaveGamePage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DataContext = new SaveGamePageViewModel((GameObject)e.Parameter);

            base.OnNavigatedTo(e);
        }
        
        private void btnSaveGame_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button) e.OriginalSource;

            SaveGame(((GameObject)button.DataContext).FileName);
        }

        private void btnBackGame_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private async void SaveGame(string fileName = null)
        {
            var result = await viewModel.SaveGameAsync(fileName);

            ShowMessage(result.saveSuccessful ? "Saved Successfully" : "Save failed");

            Frame.Navigate(typeof(MainGamePage), result.currentGame);
        }

        private void btnSaveNewGame_Click(object sender, RoutedEventArgs e)
        {
            SaveGame();
        }
    }
}