using Windows.UI.Xaml.Controls;

using Whiskey_Tycoon.UWP.ViewModels;

namespace Whiskey_Tycoon.UWP.Views
{
    public sealed partial class MainGamePage : Page
    {
        public MainGamePage()
        {
            InitializeComponent();

            DataContext = new MainGamePageViewModel();
        }
    }
}