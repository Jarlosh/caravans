using System;
using Tools.IDPools;
using Zenject;

namespace Stocks.ItemHandle
{
    public class ItemFactoryInstaller : Installer<ItemFactoryInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .BindFactory<int, int, ItemModel, ItemPool.Factory>()
                .FromPoolableMemoryPool<int, int, ItemModel, ItemPool>();
            
        }
    }
    
    public class ItemPool : PoolableMemoryPool<int, int, IMemoryPool, ItemModel>, IDisposable
    {
        private LongIDPool idPool = new LongIDPool();

        protected override void OnSpawned(ItemModel item)
        {
            item.SetIDUnsafe(idPool.AllocateID());
            base.OnSpawned(item);
        }

        // protected override void Reinitialize(int p1, int p2, IMemoryPool p3, ItemModel item)
        // {
        //     item.SetIDUnsafe(idPool.AllocateID());
        //     base.Reinitialize(p1, p2, p3, item);
        // }

        protected override void OnDespawned(ItemModel item)
        {
            idPool.ReleaseID(item.ID);
            base.OnDespawned(item);
        }

        public class Factory : PlaceholderFactory<int, int, ItemModel>
        {
            // just to give arg names to ide
            public new ItemModel Create(int itemId, int count)
            {
                return base.Create(itemId, count);
            }
        }
    }
}