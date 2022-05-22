using System;
using System.Collections.Generic;
using Stocks;
using Stocks.Inventories;
using Trade.Factory;
using UniRx;
using UnityEngine;
using Zenject;

namespace Trade
{
    public class StackViewContainerOld
    {
        [Inject] private StackViewOld.PoolFactory itemViewPool;
        [Inject] private IItemConfigManager itemManager;

        private Dictionary<ItemStack, StackViewOld> _spawned 
            = new Dictionary<ItemStack, StackViewOld>();

        public Subject<ItemStack> OnItemClicked = new Subject<ItemStack>();

        public void Spawn(Transform parent, ItemStack stack)
        {
            var desc = itemManager[stack.Item.ItemID];
            var view = itemViewPool.Create(parent, stack, desc);
            view.OnClickedEvent += OnItemClickedHandler;
            _spawned.Add(stack, view);
        }

        public void Despawn(ItemStack itemModel)
        {
            if (!_spawned.TryGetValue(itemModel, out var view))
                return;
            
            _spawned.Remove(view.Stack);
            view.OnClickedEvent -= OnItemClickedHandler;
            view.Dispose();
        }

        private void OnItemClickedHandler(StackViewOld viewOld)
        {
            OnItemClicked.OnNext(viewOld.Stack);
        }
    }
}