namespace Tools.IDPools
{
    public class LongIDPool: IDPool<long>
    {
        public long MaxReached { get; private set; }

        protected override bool CanCreateID => MaxReached < long.MaxValue;
        
        protected override long CreateID()
        {
            MaxReached++;
            return MaxReached;
        }
    }
}