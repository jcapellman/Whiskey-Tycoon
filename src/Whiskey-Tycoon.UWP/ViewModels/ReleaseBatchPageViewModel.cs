using System.Linq;

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

                var liquidAvailable = Batch.Barrels.Sum(a => (a.FillAmount / 100.0) * Constants.BARREL_SIZE_ML);

                var water = liquidAvailable * ((Constants.DEFAULT_BARREL_PROOF / SelectedProof) - 1);

                NumberBottles = (ulong)(liquidAvailable + water) / Constants.BOTTLE_SIZE;
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
            Game.ReleaseBatch(_batchObject, Price, SelectedProof, NumberBottles);
        }
    }
}