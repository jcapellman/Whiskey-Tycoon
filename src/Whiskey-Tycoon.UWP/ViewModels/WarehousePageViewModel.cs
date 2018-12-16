using Whiskey_Tycoon.lib.Containers;
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

        public WarehousePageViewModel(ManageWarehouseContainer container)
        {
            Game = container.Game;
            Warehouse = container.SelectedWarehouse;
        }
    }
}