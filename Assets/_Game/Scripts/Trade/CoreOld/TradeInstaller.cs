using UnityEngine;
using Zenject;

namespace Trade
{
    public class TradeInstaller : MonoInstaller
    {
        [SerializeField] private TradeConfig config;
        [SerializeField] private TradeControllerOldOld.Config viewsConfig;

        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<TradeControllerOldOld>()
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