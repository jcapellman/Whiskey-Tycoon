﻿using Windows.UI.Xaml;

using Whiskey_Tycoon.UWP.ViewModels;

namespace Whiskey_Tycoon.UWP.Views
{
    public sealed partial class NewGamePage : BasePage
    {
        private NewGamePageViewModel viewModel => (NewGamePageViewModel) DataContext;

        public NewGamePage()
        {
            InitializeComponent();

            DataContext = new NewGamePageViewModel();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void btnStartGame_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainGamePage), viewModel.GetGameObject());
        }
    }
}