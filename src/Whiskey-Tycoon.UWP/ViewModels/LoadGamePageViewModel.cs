using System;
using System.Collections.ObjectModel;

using Windows.Storage;
using Windows.Storage.Search;

using Newtonsoft.Json;

using Whiskey_Tycoon.lib.Common;
using Whiskey_Tycoon.lib.JSONObjects;

namespace Whiskey_Tycoon.UWP.ViewModels
{
    public class LoadGamePageViewModel : BaseViewModel
    {
        private readonly Windows.Storage.StorageFolder _storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

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
            var options = new QueryOptions();

            options.FileTypeFilter.Add($".{Constants.FILE_SAVEGAME_EXTENSION}");
            options.FolderDepth = FolderDepth.Deep;
            
            var query = _storageFolder.CreateFileQueryWithOptions(options);

            var saveGames = await query.GetFilesAsync();

            foreach (var saveGame in saveGames)
            {
                var gameObjectString = await FileIO.ReadTextAsync(saveGame);

                if (string.IsNullOrEmpty(gameObjectString))
                {
                    continue;
                }

                try
                {
                    var gameObject = JsonConvert.DeserializeObject<GameObject>(gameObjectString);

                    Games.Add(gameObject);
                }
                catch (JsonException)
                {
                    // TODO Log
                    continue;
                }
            }
        }
    }
}