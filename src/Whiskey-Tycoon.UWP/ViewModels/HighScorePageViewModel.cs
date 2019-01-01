using System.Collections.Generic;

using Whiskey_Tycoon.lib.JSONObjects;
using Whiskey_Tycoon.lib.Managers;

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
            HighScores = await HighScoreManager.GetHighScoresAsync(App.FileSystem);
        }
    }
}