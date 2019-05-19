using System.Linq;
using Windows.UI.Xaml;
using Whiskey_Tycoon.lib.Common;
using Whiskey_Tycoon.lib.Containers;
using Whiskey_Tycoon.lib.JSONObjects;

namespace Whiskey_Tycoon.UWP.ViewModels
{
    public class ReleaseBatchPageViewModel : BaseViewModel
    {
        private WarehouseObject _warehouseObject;

        private bool _btnReleaseToPressEnable;

        public bool btnReleaseToPressEnable
        {
            get => _btnReleaseToPressEnable;

            set
            {
                _btnReleaseToPressEnable = value;
                OnPropertyChanged();
            }
        }

        private BatchObject _batchObject;

        public BatchObject Batch
        {
            get => _batchObject;

            set
            {
                _batchObject = value;

                OnPropertyChanged();
            }
        }

        private ulong _numberBottles;

        public ulong NumberBottles
        {
            get => _numberBottles;

            set
            {
                _numberBottles = value;
                OnPropertyChanged();
            }
        }

        private ulong _bottlingCost;

        public ulong BottlingCost
        {
            get => _bottlingCost;

            set
            {
                _bottlingCost = value;

                OnPropertyChanged();
            }
        }

        private ulong _selectedPrice;

        public ulong SelectedPrice
        {
            get => _selectedPrice;

            set
            {
                _selectedPrice = value;

                OnPropertyChanged();
            }
        }

        private float _selectedProof;

        public float SelectedProof
        {
            get => _selectedProof;

            set
            {
                _selectedProof = value;
                OnPropertyChanged();
                
                if (value > Constants.DEFAULT_BARREL_PROOF)
                {
                    SelectedProof = Constants.DEFAULT_BARREL_PROOF;
                } else if (value < Constants.MINIMUM_PROOF)
                {
                    SelectedProof = Constants.MINIMUM_PROOF;
                }

                UpdateForm();
            }
        }
        
        private void UpdateForm()
        {
            var liquidAvailable = Batch.Barrels.Sum(a => (a.FillAmount / 100.0) * Constants.BARREL_SIZE_ML);

            var water = liquidAvailable * ((Constants.DEFAULT_BARREL_PROOF / SelectedProof) - 1);

            NumberBottles = (ulong)(liquidAvailable + water) / Constants.BOTTLE_SIZE;

            btnReleaseToPressEnable =
                !Batch.PressSampleReviews.Any(a => a.Quarter == Game.CurrentQuarter && a.Year == Game.CurrentYear);

            BottlingCost = Constants.BOTTLE_COST * NumberBottles;
            
            ListViewVisibility = _batchObject.PressSampleReviews.Any() ? Visibility.Visible : Visibility.Collapsed;
        }

        public string ReleaseBatchName => $"Release {Batch.Name}";

        public ManageWarehouseContainer Container => new ManageWarehouseContainer
        {
            Game = Game,
            SelectedWarehouse = _warehouseObject,
            SelectedBatch = _batchObject
        };

        public ReleaseBatchPageViewModel(ManageWarehouseContainer container)
        {
            Game = container.Game;
            _warehouseObject = container.SelectedWarehouse;
            _batchObject = container.SelectedBatch;

            SelectedProof = Constants.DEFAULT_BARREL_PROOF;
            SelectedPrice = Constants.DEFAULT_BOTTLE_PRICE;
        }

        public ReleasesObject ReleaseTheBatch()
        {
            return Game.ReleaseBatch(_batchObject, SelectedPrice, SelectedProof, NumberBottles, 0, BottlingCost);
        }

        public void ReleaseBatchToPress()
        {
            _batchObject.ReleaseToPress(Game.CurrentYear, Game.CurrentQuarter);
            
            UpdateForm();
        }
    }
}