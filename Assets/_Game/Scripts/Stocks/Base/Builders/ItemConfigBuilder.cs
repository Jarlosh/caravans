using System;
using System.Collections.Generic;
using System.Linq;
using Stocks.Builders.Imp;

namespace Stocks.Builders
{
    public class ItemConfigBuilder
    {
        public Dictionary<ItemExtensionType, IExtensionInfoBuilder> extensionBuilders;

        public ItemConfigBuilder()
        {
            extensionBuilders = new Dictionary<ItemExtensionType, IExtensionInfoBuilder>()
            {
                {ItemExtensionType.Food, new FoodExtensionInfoBuilder()}
            };
        }
        
        public IEnumerable<ItemInfo> Build(IEnumerable<ItemInfoSO> raw)
        {
            foreach (var itemInfoSo in raw)
                yield return BuildInfo(itemInfoSo);
        }

        private ItemInfo BuildInfo(ItemInfoSO itemInfoSo)
        {
            return new ItemInfo()
            {
                ID = itemInfoSo.ID,
                Name = itemInfoSo.Name,
                Extensions = BuildExtensions(itemInfoSo.Extensions).ToList()
            };
        }

        private IEnumerable<ItemExtensionInfoRef> BuildExtensions(List<ItemExtensionSORef> extensions)
        {
            foreach (var extensionSoAbc in extensions)
                yield return BuildExtension(extensionSoAbc);
        }

        private ItemExtensionInfoRef BuildExtension(ItemExtensionSORef extensionSoAbc)
        {
            var type = extensionSoAbc.type;
            if (!extensionBuilders.TryGetValue(type, out var extBuilder))
                throw new Exception($"No builder for extension {type}");

            return new ItemExtensionInfoRef()
            {
                type = type, 
                info = extBuilder.Build(extensionSoAbc)
            };
        }
    }
}