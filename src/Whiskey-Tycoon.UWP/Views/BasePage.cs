using System;

using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace Whiskey_Tycoon.UWP.Views
{
    public class BasePage : Page
    {
        public async void ShowMessage(string content)
        {
            var md = new MessageDialog(content);

            await md.ShowAsync();
        }
    }
}