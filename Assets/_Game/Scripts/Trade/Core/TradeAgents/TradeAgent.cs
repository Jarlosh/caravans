using System;
using Stocks;
using Stocks.Inventories;
using Stocks.TestData;

namespace Trade
{
    public class TradeAgent : TradeAgentAbc, ITradeAgent
    {
        private InventoryModel userInv;
        private InventoryModel barterInv;

        public TradeAgent(InventoryModel userInv, InventoryModel barterInv)
        {
            this.userInv = userInv;
            this.barterInv = barterInv;
        }

        public void Trade(ItemModel item)
        {
            TransferItem(userInv, barterInv, item);
        }
    }
}