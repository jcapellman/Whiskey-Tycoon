using System.Collections.Generic;

using Whiskey_Tycoon.lib.JSONObjects;

namespace Whiskey_Tycoon.UWP.ViewModels
{
    public class LoadGamePageViewModel : BaseViewModel
    {
        private List<GameObject> _games;

        public List<GameObject> Games {
            get => _games;
            set { _games = value; OnPropertyChanged(); }
        }
    }
}