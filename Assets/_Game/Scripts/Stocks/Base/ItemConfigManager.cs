using System.Collections.Generic;
using Stocks.Builders;
using Zenject;

namespace Stocks
{
    public interface IItemConfigManager : IDictionary<int, ItemInfo>
    {
        
    }

    public class ItemConfigManager : Dictionary<int, ItemInfo>, IItemConfigManager, IInitializable
    {
        private ItemConfig config;

        public ItemConfigManager(ItemConfig config)
        {
            this.config = config;
        }

        public void Initialize()
        {
            Build();
        }

        private void Build()
        {
            var builder = new ItemConfigBuilder();
            var infos = builder.Build(config.Infos);
            foreach (var info in infos) 
                Add(info.ID, info);
        }
    }
}