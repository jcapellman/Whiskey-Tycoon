using Whiskey_Tycoon.lib.Containers;
using Whiskey_Tycoon.UWP.ViewModels;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

namespace Whiskey_Tycoon.UWP.Views
{
    public sealed partial class WarehousePage : BasePage
    {
        private WarehousePageViewModel ViewModel => (WarehousePageViewModel) DataContext;

        public WarehousePage()
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

            DataContext = new WarehousePageViewModel((ManageWarehouseContainer)e.Parameter);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(WarehouseManagementPage), ViewModel.Game);
        }

        private void btnCreateBatch_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.AddBatch();
        }
    }
}