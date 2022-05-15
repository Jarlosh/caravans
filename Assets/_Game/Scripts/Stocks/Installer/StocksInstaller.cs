using _Game.Scripts;
using Stocks.ItemHandle;
using Stocks.TestData;
using Trade.Factory;
using UnityEngine;
using Zenject;

namespace Stocks.Installer
{
    
    public class StocksInstaller : MonoInstaller
    {
        [SerializeField] private ItemConfig itemConfig;
        [SerializeField] private InventoryInstaller.Config inventoryConfig;

        public override void InstallBindings()
        {
            BindCore();
            
            InventoryInstaller.Install(Container, inventoryConfig);

            BindTest();
        }

        private void BindCore()
        {
            Container
                .BindInterfacesTo<ItemConfigManager>()
                .AsSingle()
                .WithArguments(itemConfig);
            
            ItemFactoryInstaller.Install(Container);
        }

        private void BindTest()
        {
            Container
                .BindInterfacesAndSelfTo<TestInventoryDataBuilder>()
                .AsSingle();
            
            // Container
            //     .BindInterfacesTo<TestClass>()
            //     .AsSingle()
            //     .NonLazy();
        }
    }
}