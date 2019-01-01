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

        public bool NextQuarter()
        {
            var continueGame = Game.NextQuarter();

            Game = Game;

            if (continueGame)
            {
                return true;
            }

            var highScoreObject = new HighScoresObject
            {
                BottlesSold = Game.BottlesSold,
                DistillerName = Game.DistillerName,
                DistilleryName = Game.DistilleryName,
                YearsSurvived = Game.YearsOld
            };

            HighScoreManager.AddHighScore(highScoreObject, App.FileSystem);

            return false;
        }
    }
}