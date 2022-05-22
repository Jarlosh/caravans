using Stocks.Inventories;
using Stocks.TestData;
using Zenject;

namespace Trade
{
    public class TradeStarter : IInitializable
    {
        [Inject] private TestInventoryDataBuilder builder;
        [Inject] private ITradeControllerOld<TradeControllerOldOld.TradeData> _controllerOld;
        private TradeConfig config;

        public TradeStarter(TradeConfig config)
        {
            this.config = config;
        }
        
        public void Initialize()
        {
            _controllerOld.Initialize(MakeTradeData());
        }

        private TradeControllerOldOld.TradeData MakeTradeData()
        {
            return new TradeControllerOldOld.TradeData()
            {
                userInventory = MakeInventory(config.leftData),
                otherInventory = MakeInventory(config.rightData),
                barterOutInventory = MakeInventory(),
                barterInInventory = MakeInventory()
            };
        }

        private IInventoryControllerOld MakeInventory()
        {
            return new InventoryControllerOld(new InventoryModelOld());
        }

        private IInventoryControllerOld MakeInventory(TestInventoryData data)
        {
            var inv = MakeInventory();
            foreach (var itemModel in builder.Build(data)) 
                inv.AddCount(itemModel, itemModel.Count);
            return inv;
        }
    }
}