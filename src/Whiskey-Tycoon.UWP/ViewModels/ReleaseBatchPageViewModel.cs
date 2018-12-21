using Whiskey_Tycoon.lib.Containers;
using Whiskey_Tycoon.lib.JSONObjects;

namespace Whiskey_Tycoon.UWP.ViewModels
{
    public class ReleaseBatchPageViewModel : BaseViewModel
    {
        private WarehouseObject _warehouseObject;

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

        private int _numberBottles;

        public int NumberBottles
        {
            get => _numberBottles;

            set
            {
                _numberBottles = value;
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

                NumberBottles = (Batch.NumberBarrels * Whiskey_Tycoon.lib.Common.Constants.NUMBER_BOTTLES_PER_BARREL) *
                                (Batch.BarrelFillAmount / 100);
            }
        }

        private int _price;

        public int Price
        {
            get => _price;

            set
            {
                _price = value;
                OnPropertyChanged();
            }
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
        }

        public void ReleaseTheBatch()
        {
            var releaseObject = new ReleasesObject
            {
                Name = Batch.Name,
                BottlesSold = 0,
                BottlePrice = Price,
                QualityRating = Batch.Quality,
                ReleaseQuarter = Game.CurrentQuarter,
                ReleaseYear = Game.CurrentYear,
                YearsAged = Batch.BarrelQuarterAge / 4.0f
            };

            Game.Releases.Add(releaseObject);
        }
    }
}