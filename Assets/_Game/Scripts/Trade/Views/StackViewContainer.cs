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
    public class StackViewContainer
    {
        [Inject] private StackView.PoolFactory itemViewPool;
        [Inject] private IItemConfigManager itemManager;

        private Dictionary<ItemStack, StackView> _spawned 
            = new Dictionary<ItemStack, StackView>();

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

        private void OnItemClickedHandler(StackView view)
        {
            OnItemClicked.OnNext(view.Stack);
        }
    }
}