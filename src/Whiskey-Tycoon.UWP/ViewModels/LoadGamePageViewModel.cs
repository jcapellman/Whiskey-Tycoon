using System.Collections.ObjectModel;

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
    }
}