namespace Tools.IDPools
{
    public class LongIDPool: IDPool<long>
    {
        private long maxReached;

        protected override bool CanCreateID => maxReached < long.MaxValue;
        
        protected override long CreateID()
        {
            maxReached++;
            return maxReached;
        }
    }
}