using Stocks.ItemExtensions;

namespace Stocks.Builders.Imp
{
    public class FoodExtensionInfoBuilder : ExtensionInfoBuilderAbc<FoodItemExtensionSO, FoodItemExtensionInfoInfo>
    {
        protected override FoodItemExtensionInfoInfo Build(FoodItemExtensionSO so)
        {
            return new FoodItemExtensionInfoInfo()
            {
                calories = so.calories
            };
        }
    }
}