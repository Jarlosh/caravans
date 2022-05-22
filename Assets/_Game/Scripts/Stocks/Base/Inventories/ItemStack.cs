using System;
using System.Collections.Generic;
using UniRx;

namespace Stocks.Inventories
{
    public class ItemStack
    {
        public ItemModel Item { get; private set; }
        
        private ReactiveProperty<int> count;
        public IReadOnlyReactiveProperty<int> Count => count;

        public void ChangeCount(int delta)
        {
            if (count.Value + delta < 0)
                throw new Exception("Cant decrease more than stack count");
            count.Value += delta;
            Item.Count += delta;
        }
        
        public ItemStack(ItemModel item, int count)
        {
            Item = item;
            this.count = new ReactiveProperty<int>(count);
        }
    }
}