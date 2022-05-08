using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq;
using Tools.IDPools;

namespace Stocks.Inventories
{
    public interface IInventoryModel : IReadOnlyDictionary<long, ItemModel>
    {
        bool Add(ItemModel model);
        bool Remove(long localID);
        public IEnumerable<ItemModel> GetByItemID(int itemId);
    }
}