using System;
using System.Collections.Generic;
using Stocks;
using Stocks.Inventories;
using Stocks.TestData;
using Trade.Factory;
using UnityEngine;
using Zenject;

namespace Trade
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private Transform itemsViewsParent;
        [Inject] private InventoryViewContainer itemViewsContainer;

        private InventoryModel inventory;

        public event Action<InventoryView, ItemModel> OnItemClickedEvent;

        private void OnEnable()
        {
            itemViewsContainer.OnItemClickedEvent += OnItemClicked;
            SubscribeInventory();
        }

        private void OnDisable()
        {
            itemViewsContainer.OnItemClickedEvent -= OnItemClicked;
            UnsubscribeInventory();
        }

        private void SubscribeInventory()
        {
            if(inventory != null)
            {
                inventory.OnItemAdded += OnItemAdded;
                inventory.OnItemRemoved += OnItemRemoved;
            }
        }

        private void UnsubscribeInventory()
        {
            if(inventory != null)
            {
                inventory.OnItemAdded -= OnItemAdded;
                inventory.OnItemRemoved -= OnItemRemoved;
            }
        }

        public void SetInventory(InventoryModel inventory)
        {
            Clear();
            
            this.inventory = inventory;
            SubscribeInventory();
            
            foreach (var item in inventory) 
                OnItemAdded(item);
        }

        private void Clear()
        {
            if (inventory != null)
                foreach (var item in inventory)
                    OnItemRemoved(item);
        }

        private void OnItemRemoved(ItemModel item)
        {
            itemViewsContainer.Despawn(item);
        }

        private void OnItemAdded(ItemModel item)
        {
            itemViewsContainer.Spawn(itemsViewsParent, item);
        }

        private void OnItemClicked(ItemModel item)
        {
            OnItemClickedEvent?.Invoke(this, item);
        }
    }
}