using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using Whiskey_Tycoon.lib.Containers;
using Whiskey_Tycoon.UWP.ViewModels;
using Whiskey_Tycoon.lib.JSONObjects;

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

        private async void btnTrash_Click(object sender, RoutedEventArgs e)
        {
            var dialogResult = await ShowYesNoDialogAsync("Are you sure you want to trash this batch?");

            if (!dialogResult)
            {
                return;
            }

            var button = (Button)e.OriginalSource;

            var batchObject = (BatchObject)button.DataContext;

            var deletionResult = ViewModel.DeleteBatch(batchObject);

            if (!deletionResult)
            {
                ShowMessage($"{batchObject.Name} could not be trashed");
            }
        }
    }
}