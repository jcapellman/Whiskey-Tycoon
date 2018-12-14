using System;
using System.Collections.Generic;
using System.Linq;

using Whiskey_Tycoon.lib.Enums;
using Whiskey_Tycoon.lib.JSONObjects;

namespace Whiskey_Tycoon.UWP.ViewModels
{
    public class WarehouseManagementPageViewModel : BaseViewModel
    {
        private ulong _warehouseCost;

        public ulong WarehouseCost
        {
            get => _warehouseCost;

            set
            {
                _warehouseCost = value;
                OnPropertyChanged();
            }
        }

        private List<string> _warehouseSizes;

        public List<string> WarehouseSizes
        {
            get => _warehouseSizes;

            set
            {
                _warehouseSizes = value;
                OnPropertyChanged();
            }
        }

        private string _selectedWarehouseSize;

        public string SelectedWarehouseSize
        {
            get => _selectedWarehouseSize;

            set
            {
                _selectedWarehouseSize = value;
                OnPropertyChanged();

                WarehouseCost = ((WarehouseSizes) Enum.Parse(typeof(WarehouseSizes), value)).ToCost();

                ValidateForm();
            }
        }

        private string _warehouseName;

        public string WarehouseName
        {
            get => _warehouseName;

            set
            {
                _warehouseName = value;

                OnPropertyChanged();

                ValidateForm();
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
            EnableCreateButton = !string.IsNullOrEmpty(WarehouseName) && Game.MoneyAvailable > WarehouseCost;
        }

        public WarehouseManagementPageViewModel(GameObject game)
        {
            Game = game;

            WarehouseSizes = Enum.GetNames(typeof(WarehouseSizes)).ToList();

            ClearForm();
        }

        public void ClearForm()
        {
            WarehouseName = string.Empty;
            SelectedWarehouseSize = WarehouseSizes.FirstOrDefault();

            ValidateForm();
        }
    }
}