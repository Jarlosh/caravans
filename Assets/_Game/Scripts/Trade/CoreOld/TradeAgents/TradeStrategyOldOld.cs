using System;
using Stocks;
using Stocks.Inventories;
using Stocks.TestData;

namespace Trade
{
    public class TradeStrategyOldOld : ITradeStrategyOld
    {
        private IInventoryControllerOld mainInv;
        private IInventoryControllerOld targetInv;

        public TradeStrategyOldOld(IInventoryControllerOld mainInv, IInventoryControllerOld targetInv)
        {
            this.mainInv = mainInv;
            this.targetInv = targetInv;
        }

        public void OnClick(ItemStack stack)
        {
            mainInv.Transfer(targetInv, from: stack, 1);
        }
    }
}