using Stocks;
using Stocks.Inventories;

namespace Trade
{
    public interface ITradeStrategy
    {
        void OnClick(ItemStack item);
    }
}