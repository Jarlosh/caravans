using System;
using Stocks;
using Stocks.Inventories;
using Stocks.TestData;

namespace Trade
{
    public class TradeAgent : TradeAgentAbc
    {
        private IInventoryModel mainInv;
        private IInventoryModel targetInv;

        public TradeAgent(IInventoryModel mainInv, IInventoryModel targetInv)
        {
            this.mainInv = mainInv;
            this.targetInv = targetInv;
        }

        public override void Trade(ItemModel item)
        {
            TransferItem(mainInv, targetInv, item);
        }
    }
}