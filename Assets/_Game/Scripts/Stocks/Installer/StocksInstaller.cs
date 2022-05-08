using _Game.Scripts;
using Stocks.ItemHandle;
using Stocks.TestData;
using UnityEngine;
using Zenject;

namespace Stocks.Installer
{
    public class StocksInstaller : MonoInstaller
    {
        [SerializeField] private ItemConfig itemConfig;
        
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<ItemConfigManager>()
                .AsSingle()
                .WithArguments(itemConfig);
            
            ItemFactoryInstaller.Install(Container);

            BindTest();
        }

        private void BindTest()
        {
            Container
                .BindInterfacesAndSelfTo<TestInventoryDataBuilder>()
                .AsSingle();
            
            Container
                .BindInterfacesTo<TestClass>()
                .AsSingle()
                .NonLazy();
        }
    }
}