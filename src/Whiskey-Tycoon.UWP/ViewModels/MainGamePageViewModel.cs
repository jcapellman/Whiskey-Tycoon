using Whiskey_Tycoon.lib.JSONObjects;

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

            return continueGame;
        }
    }
}