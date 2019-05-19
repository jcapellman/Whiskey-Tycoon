﻿using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

using Whiskey_Tycoon.lib.Containers;
using Whiskey_Tycoon.UWP.ViewModels;

namespace Whiskey_Tycoon.UWP.Views
{
    public sealed partial class ReleaseBatchPage : BasePage
    {
        private ReleaseBatchPageViewModel ViewModel => (ReleaseBatchPageViewModel) DataContext;

        public ReleaseBatchPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (!(e.Parameter is ManageWarehouseContainer))
            {
                return;
            }

            DataContext = new ReleaseBatchPageViewModel((ManageWarehouseContainer)e.Parameter);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(WarehousePage), ViewModel.Container);
        }

        private void btnRelease_Click(object sender, RoutedEventArgs e)
        {
            var release = ViewModel.ReleaseTheBatch();

            ShowMessage($"Your release review:{System.Environment.NewLine}" +
                        $"{release.PressReviewDescription}{System.Environment.NewLine}{System.Environment.NewLine}" +
                        $"Rating:{System.Environment.NewLine}{release.PressReviewRating}");

            Frame.Navigate(typeof(MainGamePage), ViewModel.Game);
        }

        private void btnReleaseToPress_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.ReleaseBatchToPress();
        }
    }
}