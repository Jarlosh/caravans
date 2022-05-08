using System;
using System.Collections;
using System.Collections.Generic;
using Tools.IDPools;

namespace Stocks
{
    public class InventoryModel : IInventoryModel
    {
        private IDictionary<long, ItemModel> byLocalID;
        private IDictionary<int, HashSet<ItemModel>> byItemID;
        private LongIDPool localIdPool;

        public InventoryModel()
        {
            byItemID = new Dictionary<int, HashSet<ItemModel>>();
            byLocalID = new Dictionary<long, ItemModel>();
            localIdPool = new LongIDPool();
        }

        public long Add(ItemModel model)
        {
            if (!localIdPool.CanAllocateID)
                throw new Exception("Can't allocate ID");
                
            var localID = localIdPool.AllocateID();
            byLocalID.Add(localID, model);

            var itemID = model.ItemID;
            if (!byItemID.TryGetValue(itemID, out var modelList))
                modelList = byItemID[itemID] = new HashSet<ItemModel>();
            modelList.Add(model);
            
            return localID;
        }

        public bool Remove(long localID)
        {
            if (!TryGetValue(localID, out var item))
                return false;
            byLocalID.Remove(localID);
            localIdPool.ReleaseID(localID);

            if (TryGetSetByItemID(item.ItemID, out var set)) 
                set.Remove(item);
            
            return true;
        }
        
        public IEnumerable<ItemModel> GetByItemID(int itemId)
        {
            if(byItemID.TryGetValue(itemId, out var list))
                foreach (var model in list)
                    yield return model;
            yield break;
        }

        private bool TryGetSetByItemID(int itemId, out HashSet<ItemModel> set)
        {
            return (byItemID.TryGetValue(itemId, out set));
        }
        
        #region Delegated
        public IEnumerator<KeyValuePair<long, ItemModel>> GetEnumerator()
        {
            return byLocalID.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) byLocalID).GetEnumerator();
        }

        public int Count => byLocalID.Count;

        public bool ContainsKey(long key)
        {
            return byLocalID.ContainsKey(key);
        }

        public bool TryGetValue(long key, out ItemModel value)
        {
            return byLocalID.TryGetValue(key, out value);
        }

        public ItemModel this[long key] => byLocalID[key];

        public IEnumerable<long> Keys => byLocalID.Keys;

        public IEnumerable<ItemModel> Values => byLocalID.Values;
        #endregion
    }
}