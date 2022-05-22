using System;
using Stocks;
using Stocks.Inventories;
using UniRx;

namespace Trade
{
    public class TraderControllerOld : IDisposable
    {
        private IInventoryControllerOld _controllerOld;
        private InventoryViewOld _viewOld;
        private bool listening;
        private ITradeStrategyOld _strategyOld;
        private IDisposable subcription;

        public bool Listening => listening;

        public TraderControllerOld(IInventoryControllerOld controllerOld, InventoryViewOld viewOld, ITradeStrategyOld strategyOld)
        {
            this._controllerOld = controllerOld;
            this._viewOld = viewOld;
            this._strategyOld = strategyOld;
            viewOld.SetInventory(controllerOld.ModelOld);
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
            subcription = _viewOld.OnStackClicked.Subscribe(OnItemClicked);
        }

        private void Unsubcribe()
        {
            subcription.Dispose();
        }

        private void OnItemClicked(ItemStack stack)
        {
            _strategyOld.OnClick(stack);   
        }

        public void Dispose()
        {
            SetListening(false);
        }
    }
}