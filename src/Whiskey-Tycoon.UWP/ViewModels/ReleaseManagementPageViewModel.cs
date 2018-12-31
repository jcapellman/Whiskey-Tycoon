using System.Collections.ObjectModel;
using System.Linq;

using Whiskey_Tycoon.lib.JSONObjects;

namespace Whiskey_Tycoon.UWP.ViewModels
{
    public class ReleaseManagementPageViewModel : BaseViewModel
    {
        private ObservableCollection<ReleasesObject> _releases;

        public ObservableCollection<ReleasesObject> Releases
        {
            get => _releases;

            set
            {
                _releases = value;
                OnPropertyChanged();
            }
        }

        public ReleaseManagementPageViewModel(GameObject game)
        {
            Game = game;

            Releases = new ObservableCollection<ReleasesObject>(Game.Releases.Where(a => !a.Archived).OrderByDescending(a => a.ReleaseYear));
        }
    }
}