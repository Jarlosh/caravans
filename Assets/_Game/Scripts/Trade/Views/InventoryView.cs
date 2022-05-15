using System;
using System.Collections.Generic;
using Stocks;
using Stocks.Inventories;
using Stocks.TestData;
using Trade.Factory;
using UniRx;
using UnityEngine;
using Zenject;

namespace Trade
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private Transform itemsViewsParent;
        [Inject] private StackViewContainer itemViewsContainer;

        private IInventoryModel inventory;
        public Subject<ItemStack> OnStackClicked = new Subject<ItemStack>();

        private CompositeDisposable subscriptions = new CompositeDisposable();
        private IDisposable itemViewsContainerSubscription;

        private void OnEnable()
        {
            itemViewsContainerSubscription = itemViewsContainer
                .OnItemClicked
                .Subscribe(OnItemClicked);
        }

        private void OnDisable()
        {
            itemViewsContainerSubscription.Dispose();
        }

        private void SubscribeInventory()
        {
            if(inventory != null)
            {
                inventory.Stacks
                    .ObserveAdd()
                    .Subscribe(evt => OnStackAdded(evt.Value))
                    .AddTo(subscriptions);
            
                inventory.Stacks
                    .ObserveRemove()
                    .Subscribe(evt => OnStackRemoved(evt.Value))
                    .AddTo(subscriptions);
            }
        }
        
        private void UnsubscribeInventory()
        {
            if(subscriptions != null && !subscriptions.IsDisposed)
                subscriptions?.Dispose();
        }

        private void OnStackAdded(ItemStack stack)
        {
            itemViewsContainer.Spawn(itemsViewsParent, stack);
        }
        
        private void OnStackRemoved(ItemStack stack)
        {
            itemViewsContainer.Despawn(stack);
        }
        
        public void SetInventory(IInventoryModel inventory)
        {
            Clear();
            
            this.inventory = inventory;
            SubscribeInventory();
            
            foreach (var item in inventory.Stacks) 
                OnStackAdded(item);
        }

        private void Clear()
        {
            if (inventory != null)
                foreach (var item in inventory.Stacks)
                    OnStackRemoved(item);
        }

        private void OnItemClicked(ItemStack stack)
        {
            OnStackClicked.OnNext(stack);
        }
    }
}