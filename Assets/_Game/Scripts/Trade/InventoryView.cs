using System;
using System.Collections.Generic;
using Stocks;
using Stocks.Inventories;
using Stocks.TestData;
using UnityEngine;
using Zenject;

namespace Trade
{
    //todo: reimplement with zenject
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private ItemViewFactory itemsViewFactory;

        private InventoryModel inventory;
        
        public event Action<InventoryView, ItemModel> OnItemClickedEvent;

        private void Start()
        {
            itemsViewFactory.OnItemClickedEvent += OnItemClicked;
        }
        
        private void OnDestroy()
        {
            itemsViewFactory.OnItemClickedEvent -= OnItemClicked;
            if(inventory != null)
            {
                inventory.OnItemAdded -= OnItemAdded;
                inventory.OnItemRemoved -= OnItemRemoved;
            }
        }

        public void SetInventory(InventoryModel inventory)
        {
            this.inventory = inventory;
            foreach (var item in inventory) 
                OnItemAdded(item);
            
            inventory.OnItemAdded += OnItemAdded;
            inventory.OnItemRemoved += OnItemRemoved;
        }

        private void OnItemRemoved(ItemModel item)
        {
            if (!itemsViewFactory.Spawned.TryGetValue(item, out var view))
                return;
            view.OnClickedEvent -= OnItemClicked;
            itemsViewFactory.Destroy(view);
        }

        private void OnItemAdded(ItemModel item)
        {
            var view = itemsViewFactory.Spawn(item);
            view.OnClickedEvent += OnItemClicked;
        }
        
        private void OnItemClicked(InventoryItemView itemView)
        {
            OnItemClickedEvent?.Invoke(this, itemView.Item);
        }
    }
}