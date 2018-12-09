using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using Whiskey_Tycoon.lib.JSONObjects;
using Whiskey_Tycoon.UWP.ViewModels;

namespace Whiskey_Tycoon.UWP.Views
{
    public sealed partial class SaveGamePage : Page
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
        
        private async void btnSaveGame_Click(object sender, RoutedEventArgs e)
        {
            await viewModel.SaveGameAsync(((GameObject)e.OriginalSource).FileName);
        }
    }
}