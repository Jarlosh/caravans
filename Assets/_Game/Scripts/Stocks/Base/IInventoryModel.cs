using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq;
using Tools.IDPools;

namespace Stocks
{
    public interface IInventoryModel : IReadOnlyDictionary<long, ItemModel>
    {
        long Add(ItemModel model);
        bool Remove(long localID);
        public IEnumerable<ItemModel> GetByItemID(int itemId);
    }
}