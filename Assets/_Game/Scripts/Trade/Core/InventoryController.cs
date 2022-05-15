using System;
using Stocks;
using Stocks.Inventories;

namespace Trade
{
    public class InventoryController : IDisposable
    {
        public IInventoryModel model;
        private InventoryView view;
        private bool listening;
        private ITradeAgent strategy;

        public bool Listening => listening;

        public InventoryController(IInventoryModel model, InventoryView view, ITradeAgent strategy)
        {
            this.model = model;
            this.view = view;
            this.strategy = strategy;
            view.SetInventory(model);
            SetListening(true);
        }

        private void SetListening(bool value)
        {
            if (listening == value)
                return;
            listening = value;
            if (listening)
                Subscribe();
            else Unsubcribe();
        }

        public void Subscribe()
        {
            view.OnItemClickedEvent += OnItemClicked;
        }

        private void Unsubcribe()
        {
            view.OnItemClickedEvent -= OnItemClicked;
        }

        private void OnItemClicked(InventoryView view, ItemModel item)
        {
            strategy.Trade(item);   
        }

        public void Dispose()
        {
            SetListening(false);
        }
    }
}