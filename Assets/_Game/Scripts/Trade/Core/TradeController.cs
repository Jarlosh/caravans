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
            public IInventoryModel userInventory;
            public IInventoryModel otherInventory;
            public IInventoryModel barterInInventory;
            public IInventoryModel barterOutInventory;
        }

        private Config config;
        private InventoryController[] controllers;
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
                new InventoryController(tradeData.userInventory, config.userView,
                    new TradeAgent(tradeData.userInventory, tradeData.barterOutInventory)),

                new InventoryController(tradeData.otherInventory, config.otherView,
                    new TradeAgent(tradeData.otherInventory, tradeData.barterInInventory)),

                new InventoryController(tradeData.barterOutInventory, config.barterOutView,
                    new TradeAgent(tradeData.barterOutInventory, tradeData.userInventory)),

                new InventoryController(tradeData.barterInInventory, config.barterInView,
                    new TradeAgent(tradeData.barterInInventory, tradeData.otherInventory)),
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

        private void TransferAll(IInventoryModel from, IInventoryModel to)
        {
            var items = from.ToArray();
            foreach (var item in items) 
                Transfer(from, to, item);
        }

        private void Transfer(IInventoryModel from, IInventoryModel to, ItemModel item)
        {
            from.Remove(item);
            to.TryAdd(item);
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