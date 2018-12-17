using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using Whiskey_Tycoon.lib.JSONObjects;
using Whiskey_Tycoon.UWP.ViewModels;

namespace Whiskey_Tycoon.UWP.Views
{
    public sealed partial class LoadGamePage : BasePage
    {
        private LoadGamePageViewModel viewModel => (LoadGamePageViewModel) DataContext;

        public LoadGamePage()
        {
            InitializeComponent();

            DataContext = new LoadGamePageViewModel();
        }

        private void btnBackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void btnLoadGame_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)e.OriginalSource;
            
            Frame.Navigate(typeof(MainGamePage), (GameObject)button.DataContext);
        }

        private async void btnDeleteGame_Click(object sender, RoutedEventArgs e)
        {
            var dialogResult = await ShowYesNoDialogAsync("Are you sure you want to delete this save game?");

            if (!dialogResult)
            {
                return;
            }

            var button = (Button)e.OriginalSource;

            var gameObject = (GameObject) button.DataContext;

            var deletionResult = await viewModel.DeleteGameAsync(gameObject);

            if (!deletionResult)
            {
                ShowMessage($"{gameObject.SaveDisplayName} could not be deleted");
            }
        }
    }
}