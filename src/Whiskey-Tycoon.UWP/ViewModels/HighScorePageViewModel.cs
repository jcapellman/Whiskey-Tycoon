using System.Collections.ObjectModel;
using System.Linq;

using Windows.UI.Xaml;

using Whiskey_Tycoon.lib.JSONObjects;
using Whiskey_Tycoon.lib.Managers;

namespace Whiskey_Tycoon.UWP.ViewModels
{
    public class HighScorePageViewModel : BaseViewModel
    {
        private bool _enableClearHighScoreButton;

        public bool EnableClearHighScoreButton
        {
            get => _enableClearHighScoreButton;

            set
            {
                _enableClearHighScoreButton = value;

                OnPropertyChanged();
            }
        }

        private ObservableCollection<HighScoresObject> _highScores;

        public ObservableCollection<HighScoresObject> HighScores
        {
            get => _highScores;

            set
            {
                _highScores = value;
                OnPropertyChanged();
                
                ListViewVisibility = value.Any() ? Visibility.Visible : Visibility.Collapsed;
                EnableClearHighScoreButton = value.Any();
            }
        }

        public HighScorePageViewModel()
        {
            LoadHighScores();
        }

        private async void LoadHighScores()
        {
            HighScores = new ObservableCollection<HighScoresObject>(await HighScoreManager.GetHighScoresAsync(App.FileSystem));
        }

        public void ClearHighScores()
        {
            HighScoreManager.ClearHighScoresAsync(App.FileSystem);

            HighScores = new ObservableCollection<HighScoresObject>();
        }
    }
}