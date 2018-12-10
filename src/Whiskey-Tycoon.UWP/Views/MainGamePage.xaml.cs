﻿using System;

using Windows.Foundation;

using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using Whiskey_Tycoon.UWP.ViewModels;
using Whiskey_Tycoon.lib.JSONObjects;

namespace Whiskey_Tycoon.UWP.Views
{
    public sealed partial class MainGamePage : Page
    {
        private MainGamePageViewModel viewModel => (MainGamePageViewModel) DataContext;

        public MainGamePage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (!(e.Parameter is GameObject)) {
                return;
            }
            
            DataContext = new MainGamePageViewModel((GameObject)e.Parameter);
        }

        public static Rect GetElementRect(FrameworkElement element)
        {
            var buttonTransform = element.TransformToVisual(null);
            var point = buttonTransform.TransformPoint(new Point());

            return new Rect(point, new Size(element.ActualWidth, element.ActualHeight));
        }

        private async void btnOptions_Click(object sender, RoutedEventArgs e)
        {
            var menu = new PopupMenu();

            menu.Commands.Add(new UICommand("Save Game", (command) =>
            {
                Frame.Navigate(typeof(SaveGamePage), viewModel.Game);
            }));

            menu.Commands.Add(new UICommand("Quit Game", (command) =>
            {
                Frame.Navigate(typeof(MainPage));
            }));

            await menu.ShowForSelectionAsync(GetElementRect((FrameworkElement)sender));
        }

        private void btnNextQuarter_Click(object sender, RoutedEventArgs e)
        {
            viewModel.NextQuarter();
        }
    }
}