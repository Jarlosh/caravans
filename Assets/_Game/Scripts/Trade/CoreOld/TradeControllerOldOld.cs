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
    public class TradeControllerOldOld : ITradeControllerOld<TradeControllerOldOld.TradeData>, IDisposable
    {
        [Serializable]
        public class Config
        {
            public InventoryViewOld userViewOld;
            public InventoryViewOld otherViewOld;
            public InventoryViewOld barterInViewOld;
            public InventoryViewOld barterOutViewOld;
            public Button tradeButton;
        }
        
        public class TradeData
        {
            public IInventoryControllerOld userInventory;
            public IInventoryControllerOld otherInventory;
            public IInventoryControllerOld barterInInventory;
            public IInventoryControllerOld barterOutInventory;
        }

        private Config config;
        private TraderControllerOld[] controllers;
        private TradeData tradeData;

        public TradeControllerOldOld(Config config)
        {
            this.config = config;
        }

        public void Initialize(TradeData tradeData)
        {
            this.tradeData = tradeData;
            controllers = new[]
            {
                new TraderControllerOld(tradeData.userInventory, config.userViewOld,
                    new TradeStrategyOldOld(tradeData.userInventory, tradeData.barterOutInventory)),

                new TraderControllerOld(tradeData.otherInventory, config.otherViewOld,
                    new TradeStrategyOldOld(tradeData.otherInventory, tradeData.barterInInventory)),

                new TraderControllerOld(tradeData.barterOutInventory, config.barterOutViewOld,
                    new TradeStrategyOldOld(tradeData.barterOutInventory, tradeData.userInventory)),

                new TraderControllerOld(tradeData.barterInInventory, config.barterInViewOld,
                    new TradeStrategyOldOld(tradeData.barterInInventory, tradeData.otherInventory)),
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

        private void TransferAll(IInventoryControllerOld from, IInventoryControllerOld to)
        {
            var stacks = from.ModelOld.Stacks.ToArray();
            foreach (var item in stacks) 
                Transfer(from, to, item);
        }

        private void Transfer(IInventoryControllerOld from, IInventoryControllerOld to, ItemStack stack)
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