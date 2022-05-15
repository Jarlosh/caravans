using System;
using System.Collections.Generic;
using Stocks.ItemHandle;
using Zenject;

namespace Stocks.TestData
{
    public class TestInventoryDataBuilder
    {
        [Inject] private IItemConfigManager manager;
        [Inject] private ItemPool.Factory pool;

        public IEnumerable<ItemModel> Build(TestInventoryData data)
        {
            return Build(data.itemsData);
        }

        public IEnumerable<ItemModel> Build(IEnumerable<ItemModelData> data)
        {
            foreach (var itemData in data)
                yield return Build(itemData);
        }

        public ItemModel Build(ItemModelData itemData)
        {
            var itemID = itemData.item.ID;
            if (manager.ContainsKey(itemID))
                return pool.Create(itemID, itemData.Count);
            else throw new Exception($"Unknown itemID {itemID}");
        }
    }
}