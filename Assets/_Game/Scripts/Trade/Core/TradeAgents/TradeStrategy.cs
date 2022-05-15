using System;
using Stocks;
using Stocks.Inventories;
using Stocks.TestData;

namespace Trade
{
    public class TradeStrategy : ITradeStrategy
    {
        private IInventoryController mainInv;
        private IInventoryController targetInv;

        public TradeStrategy(IInventoryController mainInv, IInventoryController targetInv)
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