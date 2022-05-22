using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tools.IDPools;
using UniRx;

namespace Stocks.Inventories
{
    public class InventoryModelOld : IInventoryModelOld
    {
        public ReactiveCollection<ItemStack> Stacks { get; } = new ReactiveCollection<ItemStack>();

        public IEnumerable<ItemStack> GetStacksOf(ItemModel item)
        {
            return Stacks.Where(s => s.Item.ItemID == item.ItemID);
        }

        public bool Contains(ItemStack stack)
        {
            return Stacks.Contains(stack);
        }
    }
}