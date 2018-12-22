using Whiskey_Tycoon.lib.Common;
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

                double mlSpirits = (Batch.NumberBarrels * Constants.NUMBER_BOTTLES_PER_BARREL *
                                (Batch.BarrelFillAmount / 100) * Constants.BOTTLE_SIZE);

                if (value > Constants.DEFAULT_BARREL_PROOF)
                {
                    _selectedProof = Constants.DEFAULT_BARREL_PROOF;
                }

                mlSpirits *= (Constants.DEFAULT_BARREL_PROOF / _selectedProof);

                NumberBottles = (int) (mlSpirits / Constants.BOTTLE_SIZE);
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

            SelectedProof = Constants.DEFAULT_BARREL_PROOF;
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