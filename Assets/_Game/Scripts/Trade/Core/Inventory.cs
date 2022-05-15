using System;
using Stocks;
using Stocks.Inventories;

namespace Trade
{
    public class Inventory : IDisposable
    {
        public IInventoryModel Model { get; private set; }
        private InventoryView view;
        
        public event Action<ItemModel> OnItemClickedEvent;
        
        public Inventory(IInventoryModel model, InventoryView view)
        {
            Model = model;
            this.view = view;
            view.SetInventory(model);
            Subscribe(view);
        }

        public void Subscribe(InventoryView view)
        {
            view.OnItemClickedEvent += OnItemClicked;
        }

        private void OnItemClicked(InventoryView view, ItemModel item) 
            => OnItemClickedEvent?.Invoke(item);

        public void Dispose()
        {
            Unsubscribe();
        }

        private void Unsubscribe()
        {
            view.OnItemClickedEvent -= OnItemClicked;
        }
    }
}