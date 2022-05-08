using System;

namespace Stocks.Builders.Imp
{
    public abstract class ExtensionInfoBuilderAbc<TSO, TInfo> : IExtensionInfoBuilder
        where TSO : ItemExtensionSOAbc
        where TInfo : IItemExtensionInfo
    {
        public IItemExtensionInfo Build(ItemExtensionSORef extensionSoAbc)
        {
            var soAbc = extensionSoAbc.infoSO;
            if (soAbc == null)
                throw new NullReferenceException("Extension info SO is null");

            if (!(soAbc is TSO so))
                throw new InvalidCastException(MakeWrongTypeExcMessage(soAbc));
            return Build(so);
        }

        protected abstract TInfo Build(TSO so);

        private string MakeWrongTypeExcMessage(ItemExtensionSOAbc soAbc)
        {
            return $"Extension info so is {soAbc.GetType()} but expected {typeof(TInfo)}";
        }
    }
}