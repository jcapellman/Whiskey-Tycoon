using System;
using System.Collections.ObjectModel;
using System.Linq;

using Whiskey_Tycoon.lib.Containers;
using Whiskey_Tycoon.lib.Enums;
using Whiskey_Tycoon.lib.JSONObjects;

namespace Whiskey_Tycoon.UWP.ViewModels
{
    public class WarehousePageViewModel : BaseViewModel
    {
        private string _warehouseHeaderName;

        public string WarehouseHeaderName
        {
            get => _warehouseHeaderName;

            set
            {
                _warehouseHeaderName = $"Manage Warehouse ({value})";

                OnPropertyChanged();
            }
        }

        private WarehouseObject _warehouse;

        public WarehouseObject Warehouse
        {
            get => _warehouse;
            set
            {
                _warehouse = value;
                OnPropertyChanged();

                WarehouseHeaderName = value.Name;
            }
        }

        private ObservableCollection<string> _availableBatchTypes;

        public ObservableCollection<string> AvailableBatchTypes
        {
            get => _availableBatchTypes;
            set
            {
                _availableBatchTypes = value;
                OnPropertyChanged();
            }
        }

        private string _selectedBatchType;

        public string SelectedBatchType
        {
            get => _selectedBatchType;

            set
            {
                _selectedBatchType = value;
                OnPropertyChanged();
                
                ValidateForm();
            }
        }

        private int _numberBarrels;

        public int NumberBarrels
        {
            get => _numberBarrels;

            set
            {
                _numberBarrels = value;
                OnPropertyChanged();

                ValidateForm();
            }
        }

        private ulong _batchCost;

        public ulong BatchCost
        {
            get => _batchCost;

            set
            {
                _batchCost = value;
                OnPropertyChanged();
            }
        }

        private string _batchName;

        public string BatchName
        {
            get => _batchName;

            set
            {
                _batchName = value;

                OnPropertyChanged();
            }
        }

        private bool _enableCreateButton;

        public bool EnableCreateButton
        {
            get => _enableCreateButton;
            set
            {
                _enableCreateButton = value;
                OnPropertyChanged();
            }
        }

        private void ValidateForm()
        {
            if (NumberBarrels > Warehouse.SpaceAvailable)
            {
                NumberBarrels = Warehouse.SpaceAvailable;
            }

            BatchCost = (ulong) ((int)(BatchTypes)Enum.Parse(typeof(BatchTypes), SelectedBatchType) * NumberBarrels);

            EnableCreateButton = !string.IsNullOrEmpty(BatchName) && BatchCost <= Game.MoneyAvailable;
        }

        private void ClearForm()
        {
            BatchName = string.Empty;
            SelectedBatchType = AvailableBatchTypes.FirstOrDefault();
            NumberBarrels = 0;
        }

        public void AddBatch()
        {
            for (var x = 0; x < NumberBarrels; x++)
            {
                var barrelObject = new BarrelObject
                {
                    Quarters = 0,
                    BarrelYear = Game.CurrentYear,
                    FillAmount = 100,
                    BarrelQuarter = Game.CurrentQuarter
                };
                
                Warehouse.Barrels.Add(barrelObject);
            }

            Game.UpdateWarehouse(Warehouse);

            ClearForm();
        }

        public WarehousePageViewModel(ManageWarehouseContainer container)
        {
            Game = container.Game;
            Warehouse = container.SelectedWarehouse;
            AvailableBatchTypes = new ObservableCollection<string>(Enum.GetNames(typeof(BatchTypes)));
            
            ClearForm();
        }
    }
}