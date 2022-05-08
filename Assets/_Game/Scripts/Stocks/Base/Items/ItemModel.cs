using System;
using Stocks.ItemHandle;
using Zenject;

namespace Stocks
{
    public class ItemModel : IEquatable<ItemModel>, IDisposable, IPoolable<int, int, IMemoryPool>
    {
        IMemoryPool pool;
        
        public long ID { get; private set; }
        public int ItemID { get; private set; }
        public int Count { get; set; }

        public void OnSpawned(int itemID, int count, IMemoryPool pool)
        {
            this.pool = pool;
            this.ItemID = itemID;
            this.Count = count;
        }

        public void Dispose()
        {
            pool.Despawn(this);
        }

        public void OnDespawned()
        {
            pool = null;
        }

        public void SetIDUnsafe(long id)
        {
            ID = id;
        }

        #region IEquatable

        public override bool Equals(object obj)
        {
            return obj is ItemModel other && Equals(other);
        }

        public bool Equals(ItemModel other) => ID == other.ID;

        public override int GetHashCode() => ID.GetHashCode();

        #endregion
        
        public override string ToString()
        {
            return $"[{ID}] {ItemID} x{Count}";
        }
    }
}