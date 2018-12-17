using System.Collections.ObjectModel;
using System.Threading.Tasks;

using Whiskey_Tycoon.lib.Common;
using Whiskey_Tycoon.lib.JSONObjects;

namespace Whiskey_Tycoon.UWP.ViewModels
{
    public class LoadGamePageViewModel : BaseViewModel
    {
        private ObservableCollection<GameObject> _games;

        public ObservableCollection<GameObject> Games {
            get => _games;
            set { _games = value; OnPropertyChanged(); }
        }

        public LoadGamePageViewModel()
        {
            Games = new ObservableCollection<GameObject>();

            LoadGames();
        }

        public async void LoadGames()
        {
            var gameList = await App.FileSystem.GetFilesAsync<GameObject>(Constants.FILE_SAVEGAME_EXTENSION);

            Games = new ObservableCollection<GameObject>(gameList);
        }

        public async Task<bool> DeleteGameAsync(GameObject gameObject)
        {
            var result = await App.FileSystem.DeleteFileAsync(gameObject.FileName);

            if (result)
            {
                LoadGames();
            }

            return result;
        }
    }
}