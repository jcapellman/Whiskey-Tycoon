using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using Whiskey_Tycoon.lib.JSONObjects;
using Whiskey_Tycoon.UWP.ViewModels;

namespace Whiskey_Tycoon.UWP.Views
{
    public sealed partial class WarehouseManagementPage : Page
    {
        private WarehouseManagementPageViewModel viewModel => (WarehouseManagementPageViewModel) DataContext;

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
            viewModel.AddWarehouse();
        }
    }
}