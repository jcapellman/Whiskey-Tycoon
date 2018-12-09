using Windows.UI.Xaml;

using Whiskey_Tycoon.UWP.ViewModels;

namespace Whiskey_Tycoon.UWP.Views
{
    public sealed partial class OptionsPage : BasePage
    {
        private OptionsPageViewModel viewModel => (OptionsPageViewModel) DataContext;

        public OptionsPage()
        {
            InitializeComponent();

            DataContext = new OptionsPageViewModel();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var result = await viewModel.SaveOptionsAsync();

            ShowMessage(result ? "Options saved successfully" : "Could not save Options");
        }
    }
}