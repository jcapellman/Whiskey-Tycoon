﻿using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using Whiskey_Tycoon.lib.JSONObjects;
using Whiskey_Tycoon.UWP.ViewModels;

namespace Whiskey_Tycoon.UWP.Views
{
    public sealed partial class LoadGamePage : Page
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
            Frame.Navigate(typeof(MainGamePage), (GameObject)e.OriginalSource);
        }
    }
}