using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tools.IDPools;

namespace Stocks.Inventories
{
    public class InventoryModel : IInventoryModel
    {
        private HashSet<ItemModel> set;

        public int Count => set.Count;

        public event Action<ItemModel> OnItemAdded;
        public event Action<ItemModel> OnItemRemoved;
        
        public InventoryModel()
        {
            set = new HashSet<ItemModel>();
        }
        
        public InventoryModel(IEnumerable<ItemModel> models) : this()
        {
            set = new HashSet<ItemModel>(models);
        }

        public bool TryAdd(ItemModel model)
        {
            if (!set.Add(model))
                return false;
            OnItemAdded?.Invoke(model);
            return true;
        }

        public bool Remove(ItemModel model)
        {
            if (!set.Remove(model))
                return false;
            OnItemRemoved?.Invoke(model);
            return true;
        }

        public bool Contains(ItemModel model)
        {
            return set.Contains(model);
        }
        
        public IEnumerator<ItemModel> GetEnumerator()
        {
            return set.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}