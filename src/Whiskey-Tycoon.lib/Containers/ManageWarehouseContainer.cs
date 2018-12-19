using Whiskey_Tycoon.lib.JSONObjects;

namespace Whiskey_Tycoon.lib.Containers
{
    public class ManageWarehouseContainer
    {
        public GameObject Game { get; set; }

        public WarehouseObject SelectedWarehouse { get; set; }

        public BatchObject SelectedBatch { get; set; }
    }
}