using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Whiskey_Tycoon.UWP.Views
{
    public sealed partial class OptionsPage : Page
    {
        public OptionsPage()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}