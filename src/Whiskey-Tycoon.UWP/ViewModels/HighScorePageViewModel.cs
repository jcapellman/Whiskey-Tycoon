using System.Collections.Generic;
using System.Linq;

using Whiskey_Tycoon.lib.Common;
using Whiskey_Tycoon.lib.JSONObjects;

namespace Whiskey_Tycoon.UWP.ViewModels
{
    public class HighScorePageViewModel : BaseViewModel
    {
        private List<HighScoresObject> _highScores;

        public List<HighScoresObject> HighScores
        {
            get => _highScores;

            set
            {
                _highScores = value;
                OnPropertyChanged();
            }
        }

        public HighScorePageViewModel()
        {
            LoadHighScores();
        }

        private async void LoadHighScores()
        {
            var highScoresResult = await App.FileSystem.GetFileAsync<List<HighScoresObject>>(Constants.FILENAME_HIGHSCORES);

            HighScores = highScoresResult?.OrderByDescending(a => a.BottlesSold).ToList() ?? new List<HighScoresObject>();
        }
    }
}