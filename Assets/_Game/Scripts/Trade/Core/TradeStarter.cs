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
                userInventory = MakeModel(config.leftData),
                otherInventory = MakeModel(config.rightData),
                barterOutInventory = MakeModel(),
                barterInInventory = MakeModel()
            };
        }

        private InventoryModel MakeModel()
        {
            return new InventoryModel();
        }

        private InventoryModel MakeModel(TestInventoryData data)
        {
            var inv = MakeModel();
            foreach (var itemModel in builder.Build(data)) 
                inv.TryAdd(itemModel);
            return inv;
        }
    }
}