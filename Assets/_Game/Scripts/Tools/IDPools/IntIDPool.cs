namespace Tools.IDPools
{
    public class IntIDPool: IDPool<int>
    {
        private int maxReached;

        protected override bool CanCreateID => maxReached < int.MaxValue;
        
        protected override int CreateID()
        {
            maxReached++;
            return maxReached;
        }
    }
}