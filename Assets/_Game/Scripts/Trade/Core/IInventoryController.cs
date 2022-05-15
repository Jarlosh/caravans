using Stocks;
using Stocks.Inventories;

namespace Trade
{
    public interface IInventoryController
    {
        IInventoryModel Model { get; }

        void RemoveCount(ItemModel item, int count);
        void DecreaseStack(ItemStack stack, int count);
        void AddCount(ItemModel item, int count);

        void Transfer(IInventoryController other, ItemStack from, int count);
    }
}