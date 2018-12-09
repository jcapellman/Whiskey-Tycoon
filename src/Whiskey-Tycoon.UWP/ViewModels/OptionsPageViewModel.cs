using System.Threading.Tasks;

using Whiskey_Tycoon.lib.Common;
using Whiskey_Tycoon.lib.JSONObjects;

namespace Whiskey_Tycoon.UWP.ViewModels
{
    public class OptionsPageViewModel : BaseViewModel
    {
        private OptionsObject _options;

        public OptionsObject Options
        {
            get => _options;

            set
            {
                _options = value;
                OnPropertyChanged();
            }
        }

        public OptionsPageViewModel()
        {
            LoadOptions();
        }

        private async void LoadOptions()
        {
            var optionsResult = await App.FileSystem.GetFileAsync<OptionsObject>(Constants.FILENAME_OPTIONS);

            Options = optionsResult ?? new OptionsObject();
        }

        public async Task<bool> SaveOptionsAsync() => await App.FileSystem.WriteFileAsync<OptionsObject>(Constants.FILENAME_OPTIONS, Options);
    }
}