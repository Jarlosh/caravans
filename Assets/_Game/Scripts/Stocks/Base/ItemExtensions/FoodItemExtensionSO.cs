using UnityEngine;

namespace Stocks.ItemExtensions
{
    [CreateAssetMenu(menuName = "SO/ItemExts/Food", fileName = "FoodItemExtensionSO", order = 0)]
    public class FoodItemExtensionSO : ItemExtensionSOAbc
    {
        public int calories;
    }
    
    
    public class FoodItemExtensionInfoInfo : IItemExtensionInfo
    {
        public int calories = 100;
    }
}