using System;
using Stocks;
using Stocks.Inventories;
using UnityEngine;
using Zenject;

namespace Trade.Factory
{
    public class InventoryInstaller : Installer<InventoryInstaller.Config, InventoryInstaller>
    {
        private Config config;

        public InventoryInstaller(Config config)
        {
            this.config = config;
        }

        public override void InstallBindings()
        {
            BindItemViews();
        }

        private void BindItemViews()
        {
            Container
                .Bind<StackViewContainerOld>()
                .AsTransient();
                
            Container
                .BindFactory<Transform, ItemStack, ItemInfo, StackViewOld, StackViewOld.PoolFactory>()
                .FromMonoPoolableMemoryPool(x => x
                    .WithInitialSize(2)
                    .FromComponentInNewPrefab(config.itemViewPrefab)
                );
        }

        [Serializable]
        public class Config
        {
            public GameObject itemViewPrefab;
        }
    }
}