using _Game.Scripts;
using Stocks.ItemHandle;
using UnityEngine;
using Zenject;

namespace Stocks.Installer
{
    public class StocksInstaller : MonoInstaller
    {
        [SerializeField] private ItemConfig itemConfig;
        
        public override void InstallBindings()
        {
            ItemFactoryInstaller.Install(Container);
            
            Container
                .BindInterfacesTo<ItemConfigManager>()
                .AsSingle()
                .WithArguments(itemConfig);

            Container
                .BindInterfacesTo<TestClass>()
                .AsSingle()
                .NonLazy();
        }
    }
}