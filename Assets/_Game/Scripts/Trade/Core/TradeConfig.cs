using System;
using Stocks.TestData;
using UnityEngine;

namespace Trade
{
    [Serializable]
    public class TradeConfig
    {
        public TestInventoryData leftData;
        public TestInventoryData rightData;
    }
}