using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using Whiskey_Tycoon.lib.JSONObjects;
using Whiskey_Tycoon.UWP.ViewModels;

namespace Whiskey_Tycoon.UWP.Views
{
    public sealed partial class WarehouseManagementPage : Page
    {
        private WarehouseManagementPageViewModel ViewModel => (WarehouseManagementPageViewModel) DataContext;

        public WarehouseManagementPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (!(e.Parameter is GameObject))
            {
                return;
            }

            DataContext = new WarehouseManagementPageViewModel((GameObject)e.Parameter);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void btnCreateWarehouse_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.AddWarehouse();
        }
        
        private async void btnDemolish_Click(object sender, RoutedEventArgs e)
        {
            var warehouse = (WarehouseObject)((Button) sender).DataContext;

            var messageDialog = new MessageDialog($"Are you sure you want to delete the {warehouse.Name} warehouse?");

            messageDialog.Commands.Add(new UICommand("Yes"));
            messageDialog.Commands.Add(new UICommand("No"));

            var result = await messageDialog.ShowAsync();

            if (result.Label == "No")
            {
                return;
            }

            ViewModel.RemoveWarehouse(warehouse);
        }
    }
}