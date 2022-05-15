using UnityEngine;
using Zenject;

namespace Trade
{
    public class TradeInstaller : MonoInstaller
    {
        [SerializeField] private TradeConfig config;
        [SerializeField] private TradeController.Config viewsConfig;

        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<TradeController>()
                .AsSingle()
                .WithArguments(viewsConfig);

            Container
                .BindInterfacesTo<TradeStarter>()
                .AsSingle()
                .WithArguments(config)
                .NonLazy();
        }
    }
}