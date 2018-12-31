using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

using Whiskey_Tycoon.lib.JSONObjects;
using Whiskey_Tycoon.UWP.ViewModels;

namespace Whiskey_Tycoon.UWP.Views
{
    public sealed partial class LoanManagementPage : BasePage
    {
        private LoanManagementPageViewModel ViewModel => (LoanManagementPageViewModel)DataContext;

        public LoanManagementPage()
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

            DataContext = new LoanManagementPageViewModel((GameObject)e.Parameter);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainGamePage), ViewModel.Game);
        }

        private void btnGetLoan_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.AddLoan();

            ShowMessage($"{ViewModel.SelectedLoan.Name} obtained!");

            Frame.Navigate(typeof(MainGamePage), ViewModel.Game);
        }
    }
}