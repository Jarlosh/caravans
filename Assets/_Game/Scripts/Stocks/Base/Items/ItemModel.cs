using System;

namespace Stocks
{
    public struct ItemModel : IEquatable<ItemModel>
    {
        public long ID { get; private set; }
        public int ItemID { get; private set; }
        public int Count { get; set; }

        public ItemModel(long id, int itemID, int count=1)
        {
            ID = id;
            ItemID = itemID;
            Count = count;
        }


        public override bool Equals(object obj)
        {
            return obj is ItemModel other && Equals(other);
        }

        public bool Equals(ItemModel other) => ID == other.ID;

        public override int GetHashCode() => ID.GetHashCode();
    }
}