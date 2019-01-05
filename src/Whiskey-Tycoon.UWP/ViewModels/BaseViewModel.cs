using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Whiskey_Tycoon.lib.Common;
using Whiskey_Tycoon.lib.JSONObjects;

namespace Whiskey_Tycoon.UWP.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        private Visibility _emptyListViewVisibility;

        public Visibility EmptyListViewVisibility
        {
            get => _emptyListViewVisibility;

            set
            {
                _emptyListViewVisibility = value;

                OnPropertyChanged();
            }
        }

        private Visibility _listViewVisibility;

        public Visibility ListViewVisibility
        {
            get => _listViewVisibility;

            set
            {
                _listViewVisibility = value;

                OnPropertyChanged();

                EmptyListViewVisibility = value == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        private GameObject _gameObject;

        public GameObject Game
        {
            get => _gameObject;

            set
            {
                _gameObject = value;
                OnPropertyChanged();
            }
        }

        protected async Task<OptionsObject> GetOptions() => await App.FileSystem.GetFileAsync<OptionsObject>(Constants.FILENAME_OPTIONS);

        public BaseViewModel()
        {
            EmptyListViewVisibility = Visibility.Collapsed;
            ListViewVisibility = Visibility.Collapsed;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}