using System;
using System.Collections.ObjectModel;
using System.Linq;

using Whiskey_Tycoon.lib.Enums;

namespace Whiskey_Tycoon.lib.JSONObjects
{
    public class WarehouseObject
    {
        public Guid ID { get; set; }

        public string Name { get; set; }

        public ObservableCollection<BatchObject> Batches { get; set; }

        public WarehouseSizes Size { get; set; }

        public int BarrelsAging => Batches.Sum(a => a.Barrels.Count);

        public int SpaceAvailable => (int) Size - BarrelsAging;

        public void AgeBatches()
        {
            foreach (var batch in Batches)
            {
                batch.AgeBatch();
            }
        }

        public WarehouseObject()
        {
            ID = Guid.NewGuid();

            Batches = new ObservableCollection<BatchObject>();
        }
    }
}