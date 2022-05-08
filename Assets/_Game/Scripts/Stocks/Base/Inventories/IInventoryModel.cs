using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq;
using Tools.IDPools;

namespace Stocks.Inventories
{
    public interface IInventoryModel : IEnumerable<ItemModel>
    {
        int Count { get; }
        
        bool TryAdd(ItemModel model);
        bool Remove(ItemModel model);
        bool Contains(ItemModel model);

        public event Action<ItemModel> OnItemAdded;
        public event Action<ItemModel> OnItemRemoved;
    }
}