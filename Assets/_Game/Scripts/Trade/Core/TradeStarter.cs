using Stocks.Inventories;
using Stocks.TestData;
using Zenject;

namespace Trade
{
    public class TradeStarter : IInitializable
    {
        [Inject] private TestInventoryDataBuilder builder;
        [Inject] private ITradeController<TradeController.TradeData> controller;
        private TradeConfig config;

        public TradeStarter(TradeConfig config)
        {
            this.config = config;
        }
        
        public void Initialize()
        {
            controller.Initialize(MakeTradeData());
        }

        private TradeController.TradeData MakeTradeData()
        {
            return new TradeController.TradeData()
            {
                userInventory = MakeInventory(config.leftData),
                otherInventory = MakeInventory(config.rightData),
                barterOutInventory = MakeInventory(),
                barterInInventory = MakeInventory()
            };
        }

        private IInventoryController MakeInventory()
        {
            return new InventoryController(new InventoryModel());
        }

        private IInventoryController MakeInventory(TestInventoryData data)
        {
            var inv = MakeInventory();
            foreach (var itemModel in builder.Build(data)) 
                inv.AddCount(itemModel, itemModel.Count);
            return inv;
        }
    }
}