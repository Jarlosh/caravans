using System;
using System.Linq;
using Stocks;
using Stocks.Inventories;
using Stocks.TestData;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Trade
{
    public class TradeController : ITradeController<TradeController.TradeData>, IDisposable
    {
        [Serializable]
        public class Config
        {
            public InventoryView userView;
            public InventoryView otherView;
            public InventoryView barterInView;
            public InventoryView barterOutView;
            public Button tradeButton;
        }
        
        public class TradeData
        {
            public IInventoryController userInventory;
            public IInventoryController otherInventory;
            public IInventoryController barterInInventory;
            public IInventoryController barterOutInventory;
        }

        private Config config;
        private TraderController[] controllers;
        private TradeData tradeData;

        public TradeController(Config config)
        {
            this.config = config;
        }

        public void Initialize(TradeData tradeData)
        {
            this.tradeData = tradeData;
            controllers = new[]
            {
                new TraderController(tradeData.userInventory, config.userView,
                    new TradeStrategy(tradeData.userInventory, tradeData.barterOutInventory)),

                new TraderController(tradeData.otherInventory, config.otherView,
                    new TradeStrategy(tradeData.otherInventory, tradeData.barterInInventory)),

                new TraderController(tradeData.barterOutInventory, config.barterOutView,
                    new TradeStrategy(tradeData.barterOutInventory, tradeData.userInventory)),

                new TraderController(tradeData.barterInInventory, config.barterInView,
                    new TradeStrategy(tradeData.barterInInventory, tradeData.otherInventory)),
            };

            config.tradeButton.onClick.AddListener(OnTradeClick);
        }

        private void OnTradeClick()
        {
            if(!IsDealPossible())
                return;

            TransferAll(tradeData.barterInInventory, tradeData.userInventory);
            TransferAll(tradeData.barterOutInventory, tradeData.otherInventory);
        }

        private void TransferAll(IInventoryController from, IInventoryController to)
        {
            var stacks = from.Model.Stacks.ToArray();
            foreach (var item in stacks) 
                Transfer(from, to, item);
        }

        private void Transfer(IInventoryController from, IInventoryController to, ItemStack stack)
        {
            to.AddCount(stack.Item, stack.Count.Value);
            from.DecreaseStack(stack, stack.Count.Value);
        }

        private bool IsDealPossible()
        {
            return true;
        }

        public void Dispose()
        {
            config.tradeButton.onClick.RemoveListener(OnTradeClick);
            
            foreach (var controller in controllers) 
                controller.Dispose();
        }
    }

} 