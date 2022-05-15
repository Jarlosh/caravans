
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Stocks.TestData
{
    [Serializable]
    public class ItemModelData
    {
        public ItemInfoSO item;
        public int Count;
    }
    
    [CreateAssetMenu(menuName = "SO/Test/InventoryData", fileName = "TestInventoryData", order = 0)]
    public class TestInventoryData : ScriptableObject
    {
        public List<ItemModelData> itemsData;
    }
}