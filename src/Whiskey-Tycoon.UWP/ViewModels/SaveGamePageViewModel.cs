﻿using System.Collections.ObjectModel;
using System.Threading.Tasks;

using Whiskey_Tycoon.lib.Common;
using Whiskey_Tycoon.lib.JSONObjects;

namespace Whiskey_Tycoon.UWP.ViewModels
{
    public class SaveGamePageViewModel : BaseViewModel
    {
        private GameObject _currentGame;

        private ObservableCollection<GameObject> _games;

        public ObservableCollection<GameObject> Games
        {
            get => _games;

            set
            {
                _games = value;
                OnPropertyChanged();
            }
        }

        public SaveGamePageViewModel(GameObject game)
        {
            _currentGame = game;

            LoadGames();
        }

        private async void LoadGames()
        {
            var gameList = await App.FileSystem.GetFilesAsync<GameObject>(Constants.FILE_SAVEGAME_EXTENSION);

            Games = new ObservableCollection<GameObject>(gameList);
        }

        public async Task<bool> SaveGameAsync(string fileName) => await App.FileSystem.WriteFileAsync<GameObject>(fileName, _currentGame);
    }
}