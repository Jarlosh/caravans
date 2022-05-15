using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq;
using Tools.IDPools;
using UniRx;

namespace Stocks.Inventories
{
    public interface IInventoryModel
    {
        ReactiveCollection<ItemStack> Stacks { get; }

        IEnumerable<ItemStack> GetStacksOf(ItemModel item);
        
        bool Contains(ItemStack stack);
    }
}