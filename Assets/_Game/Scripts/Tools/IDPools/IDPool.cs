using System;
using System.Collections.Generic;
using System.Linq;

namespace Tools.IDPools
{
    public abstract class IDPool<T>
    {
        private Stack<T> released = new Stack<T>();

        public bool CanAllocateID => released.Any() || CanCreateID;

        protected abstract bool CanCreateID { get; }
        
        public T AllocateID()
        {
            if (released.Any())
                return released.Pop();
            else if (CanAllocateID)
                return CreateID();
            throw new Exception("IDs is out!");
        }

        protected abstract T CreateID();

        public void ReleaseID(T id)
        {
            released.Push(id);
        } 
    }
}