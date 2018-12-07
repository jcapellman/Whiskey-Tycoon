using System.ComponentModel;
using System.Runtime.CompilerServices;

using Whiskey_Tycoon.lib.JSONObjects;

namespace Whiskey_Tycoon.UWP.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}