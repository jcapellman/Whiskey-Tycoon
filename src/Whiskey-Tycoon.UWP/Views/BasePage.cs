using System;
using System.Threading.Tasks;

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

        public async Task<bool> ShowYesNoDialogAsync(string content)
        {
            var messageDialog = new MessageDialog(content);

            messageDialog.Commands.Add(new UICommand("Yes"));
            messageDialog.Commands.Add(new UICommand("No"));

            var result = await messageDialog.ShowAsync();

            return result.Label != "No";
        }
    }
}