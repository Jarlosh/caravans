using Stocks;
using Stocks.Inventories;

namespace Trade
{
    public interface IInventoryControllerOld
    {
        IInventoryModelOld ModelOld { get; }

        void RemoveCount(ItemModel item, int count);
        void DecreaseStack(ItemStack stack, int count);
        void AddCount(ItemModel item, int count);

        void Transfer(IInventoryControllerOld other, ItemStack from, int count);
    }
}