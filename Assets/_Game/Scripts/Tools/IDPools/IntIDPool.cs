namespace Tools.IDPools
{
    public class IntIDPool: IDPool<int>
    {
        public int MaxReached { get; private set; }

        protected override bool CanCreateID => MaxReached < int.MaxValue;
        
        protected override int CreateID()
        {
            MaxReached++;
            return MaxReached;
        }
    }
}