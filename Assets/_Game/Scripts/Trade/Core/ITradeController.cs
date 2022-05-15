namespace Trade
{
    public interface ITradeController<DataT>
    {
        void Initialize(DataT makeTradeData);
    }
}