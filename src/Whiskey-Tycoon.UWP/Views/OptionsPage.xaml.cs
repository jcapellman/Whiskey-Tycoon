using System;

using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using Whiskey_Tycoon.UWP.ViewModels;

namespace Whiskey_Tycoon.UWP.Views
{
    public sealed partial class OptionsPage : Page
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

            MessageDialog md = new MessageDialog("Options saved");

            await md.ShowAsync();
        }
    }
}