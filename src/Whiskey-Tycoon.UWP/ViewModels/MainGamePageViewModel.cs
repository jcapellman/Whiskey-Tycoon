using System.Threading.Tasks;

using Whiskey_Tycoon.lib.JSONObjects;
using Whiskey_Tycoon.lib.Managers;

namespace Whiskey_Tycoon.UWP.ViewModels
{
    public class MainGamePageViewModel : BaseViewModel
    {
        private OptionsObject _options;

        public MainGamePageViewModel(GameObject gameObject)
        {
            Game = gameObject;

            LoadOptions();
        }

        private async void LoadOptions()
        {
            _options = await GetOptions();
        }

        public async Task<(bool GameContinues, bool SaveSuccess)> NextQuarterAsync()
        {
            var continueGame = Game.NextQuarter();

            Game = Game;

            if (continueGame)
            {
                if (_options.AutoSave && !string.IsNullOrEmpty(Game.FileName))
                {
                    await App.FileSystem.WriteFileAsync<GameObject>(Game.FileName, Game);
                }
                
                return (true, false);
            }

            var highScoreObject = new HighScoresObject
            {
                BottlesSold = Game.BottlesSold,
                DistillerName = Game.DistillerName,
                DistilleryName = Game.DistilleryName,
                YearsSurvived = Game.YearsOld
            };

            var addScoreResult = await HighScoreManager.AddHighScoreAsync(highScoreObject, App.FileSystem);
            
            return (false, addScoreResult);
        }
    }
}