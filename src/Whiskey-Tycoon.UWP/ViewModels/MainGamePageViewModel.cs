using Whiskey_Tycoon.lib.JSONObjects;

namespace Whiskey_Tycoon.UWP.ViewModels
{
    public class MainGamePageViewModel : BaseViewModel
    {
        private GameObject _gameObject;

        public GameObject Game
        {
            get { return _gameObject; }
            set
            {
                _gameObject = value;
                OnPropertyChanged();
            }
        }

        public MainGamePageViewModel(GameObject gameObject)
        {
            Game = gameObject;
        }
    }
}