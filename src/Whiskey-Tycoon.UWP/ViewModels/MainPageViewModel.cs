using Windows.ApplicationModel;

namespace Whiskey_Tycoon.UWP.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public MainPageViewModel()
        {
            VersionString =
                $"Version {Package.Current.Id.Version.Major}.{Package.Current.Id.Version.Minor}.{Package.Current.Id.Version.Build}.{Package.Current.Id.Version.Revision}";
        }

        private string _versionString;

        public string VersionString
        {
            get => _versionString;

            set
            {
                _versionString = value;

                OnPropertyChanged();
            }
        }
    }
}