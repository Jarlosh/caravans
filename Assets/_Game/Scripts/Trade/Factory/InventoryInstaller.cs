using System;
using Stocks;
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
                .BindInterfacesAndSelfTo<InventoryViewContainer>()
                .AsTransient();
                
            Container
                .BindFactory<Transform, ItemModel, ItemInfo, InventoryItemView, InventoryItemView.PoolFactory>()
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