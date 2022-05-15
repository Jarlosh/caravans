using Stocks;
using Stocks.Inventories;

namespace Trade
{
    public abstract class TradeAgentAbc : ITradeAgent
    {
        protected void TransferItem(Inventory from, Inventory to, ItemModel item)
        {
            TransferItem(from.Model, to.Model, item);
        }

        protected void TransferItem(IInventoryModel from, IInventoryModel to, ItemModel item)
        {
            from.Remove(item);
            to.TryAdd(item);
        }

        public abstract void Trade(ItemModel item);
    }
}