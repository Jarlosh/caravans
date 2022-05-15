using System;
using System.Collections.Generic;
using Stocks;
using Trade.Factory;
using UnityEngine;
using Zenject;

namespace Trade
{
    public class InventoryViewContainer
    {
        [Inject] private InventoryItemView.PoolFactory itemViewPool;
        [Inject] private IItemConfigManager itemManager;

        private Dictionary<ItemModel, InventoryItemView> _spawned 
            = new Dictionary<ItemModel, InventoryItemView>();

        public event Action<ItemModel> OnItemClickedEvent;
        
        public void Spawn(Transform parent, ItemModel itemModel)
        {
            var desc = itemManager[itemModel.ItemID];
            var view = itemViewPool.Create(parent, itemModel, desc);
            view.OnClickedEvent += OnItemClicked;
            
            _spawned.Add(itemModel, view);
        }

        public void Despawn(ItemModel itemModel)
        {
            if (!_spawned.TryGetValue(itemModel, out var view))
                return;
            
            _spawned.Remove(view.Item);
            
            view.OnClickedEvent -= OnItemClicked;
            view.Dispose();
        }

        private void OnItemClicked(InventoryItemView view)
        {
            OnItemClickedEvent?.Invoke(view.Item);
        }
    }
}