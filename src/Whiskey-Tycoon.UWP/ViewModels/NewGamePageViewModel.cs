namespace Whiskey_Tycoon.UWP.ViewModels
{
    public class NewGamePageViewModel : BaseViewModel
    {
        private string _masterDistillerName;

        public string MasterDistillerName
        {
            get => _masterDistillerName;

            set
            {
                _masterDistillerName = value;
                OnPropertyChanged();

                UpdateForm(); 
            }
        }

        private string _distilleryName;

        public string DistilleryName
        {
            get => _distilleryName;

            set
            {
                _distilleryName = value;
                OnPropertyChanged();

                UpdateForm();
            }
        }

        private bool _enableStartGame;

        public bool EnableStartGame
        {
            get => _enableStartGame;
            set
            {
                _enableStartGame = value;
                OnPropertyChanged();
            }
        }

        public NewGamePageViewModel()
        {
            UpdateForm();
        }

        private void UpdateForm()
        {
            EnableStartGame = !string.IsNullOrEmpty(MasterDistillerName) && !string.IsNullOrEmpty(DistilleryName);
        }
    }
}