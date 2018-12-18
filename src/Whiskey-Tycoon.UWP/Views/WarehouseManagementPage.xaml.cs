using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using Whiskey_Tycoon.lib.JSONObjects;
using Whiskey_Tycoon.UWP.ViewModels;
using Whiskey_Tycoon.lib.Containers;

namespace Whiskey_Tycoon.UWP.Views
{
    public sealed partial class WarehouseManagementPage : BasePage
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
            Frame.Navigate(typeof(MainGamePage), ViewModel.Game);
        }

        private void btnCreateWarehouse_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.AddWarehouse();
        }
        
        private async void btnDemolish_Click(object sender, RoutedEventArgs e)
        {
            var warehouse = (WarehouseObject)((Button) sender).DataContext;

            var result = await ShowYesNoDialogAsync($"Are you sure you want to delete the {warehouse.Name} warehouse?");

            if (!result)
            {
                return;
            }

            ViewModel.RemoveWarehouse(warehouse);
        }

        private void btnManage_Click(object sender, RoutedEventArgs e)
        {
            var warehouse = (WarehouseObject)((Button)sender).DataContext;

            Frame.Navigate(typeof(WarehousePage), new ManageWarehouseContainer
            {
                Game = ViewModel.Game,
                SelectedWarehouse = warehouse
            });
        }
    }
}