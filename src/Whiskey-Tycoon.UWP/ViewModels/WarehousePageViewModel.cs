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
        private ObservableCollection<string> _availableIngredientQualityLevels;

        public ObservableCollection<string> AvailableIngredientQualityLevels
        {
            get => _availableIngredientQualityLevels;

            set
            {
                _availableIngredientQualityLevels = value;

                OnPropertyChanged();
            }
        }

        private string _selectedIngredientQualityLevel;

        public string SelectedIngredientQualityLevel
        {
            get => _selectedIngredientQualityLevel;

            set
            {
                _selectedIngredientQualityLevel = value;
                OnPropertyChanged();

                ValidateForm();
            }
        }

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

            var qualityLevel =
                (IngredientsQualityLevels) Enum.Parse(typeof(IngredientsQualityLevels), SelectedIngredientQualityLevel ?? AvailableIngredientQualityLevels.FirstOrDefault());

            BatchCost = (ulong) ((int)(BatchTypes)Enum.Parse(typeof(BatchTypes), SelectedBatchType) * NumberBarrels * qualityLevel.ToQualityLevelMultiplier());

            EnableCreateButton = !string.IsNullOrEmpty(BatchName) && BatchCost <= Game.MoneyAvailable;
        }

        private void ClearForm()
        {
            BatchName = string.Empty;
            SelectedBatchType = AvailableBatchTypes.FirstOrDefault();
            NumberBarrels = 0;
            SelectedIngredientQualityLevel = AvailableIngredientQualityLevels.FirstOrDefault();
        }

        public void AddBatch()
        {
            var batch = new BatchObject
            {
                Name = BatchName,
                BarrelQuarterAge = 0,
                BarrelQuarter = Game.CurrentQuarter,
                BarrelYear = Game.CurrentYear,
                BatchType = (BatchTypes) Enum.Parse(typeof(BatchTypes), SelectedBatchType),
                Barrels = Enumerable.Repeat(new BarrelObject(), NumberBarrels).ToList(),
                QualityLevel = (IngredientsQualityLevels)Enum.Parse(typeof(IngredientsQualityLevels), SelectedIngredientQualityLevel)
            };

            batch.Quality = batch.QualityLevel.ToQuality();

            Warehouse.Batches.Add(batch);

            Game.UpdateWarehouse(Warehouse);

            ClearForm();
        }

        public WarehousePageViewModel(ManageWarehouseContainer container)
        {
            Game = container.Game;
            Warehouse = container.SelectedWarehouse;

            AvailableBatchTypes = new ObservableCollection<string>(Enum.GetNames(typeof(BatchTypes)));
            AvailableIngredientQualityLevels = new ObservableCollection<string>(Enum.GetNames(typeof(IngredientsQualityLevels)));
            
            ClearForm();
        }
    }
}