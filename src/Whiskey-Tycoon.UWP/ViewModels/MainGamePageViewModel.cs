using System.Threading.Tasks;

using Whiskey_Tycoon.lib.JSONObjects;
using Whiskey_Tycoon.lib.Managers;

namespace Whiskey_Tycoon.UWP.ViewModels
{
    public class MainGamePageViewModel : BaseViewModel
    {
        public MainGamePageViewModel(GameObject gameObject)
        {
            Game = gameObject;
        }

        public async Task<(bool GameContinues, bool SaveSuccess)> NextQuarterAsync()
        {
            var continueGame = Game.NextQuarter();

            Game = Game;

            if (continueGame)
            {
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