using System;
using Stocks;
using Stocks.Inventories;
using UniRx;

namespace Trade
{
    public class TraderController : IDisposable
    {
        private IInventoryController controller;
        private InventoryView view;
        private bool listening;
        private ITradeStrategy strategy;
        private IDisposable subcription;

        public bool Listening => listening;

        public TraderController(IInventoryController controller, InventoryView view, ITradeStrategy strategy)
        {
            this.controller = controller;
            this.view = view;
            this.strategy = strategy;
            view.SetInventory(controller.Model);
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
            subcription = view.OnStackClicked.Subscribe(OnItemClicked);
        }

        private void Unsubcribe()
        {
            subcription.Dispose();
        }

        private void OnItemClicked(ItemStack stack)
        {
            strategy.OnClick(stack);   
        }

        public void Dispose()
        {
            SetListening(false);
        }
    }
}