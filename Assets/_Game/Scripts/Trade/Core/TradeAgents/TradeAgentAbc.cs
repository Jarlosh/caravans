using Stocks;
using Stocks.Inventories;

namespace Trade
{
    public abstract class TradeAgentAbc
    {
        protected void TransferItem(Inventory from, Inventory to, ItemModel item)
        {
            TransferItem(from.Model, to.Model, item);
        }

        protected void TransferItem(InventoryModel from, InventoryModel to, ItemModel item)
        {
            from.Remove(item);
            to.TryAdd(item);
        }
    }
}