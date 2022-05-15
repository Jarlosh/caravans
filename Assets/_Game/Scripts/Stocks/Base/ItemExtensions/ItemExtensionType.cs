using System;
using UnityEngine;

namespace Stocks
{
    public enum ItemExtensionType
    {
        Food, Fuel
    }

    [Serializable]
    public class ItemExtensionSORef
    {
        public ItemExtensionType type;
        public ItemExtensionSOAbc infoSO;
    }

    public class ItemExtensionInfoRef
    {
        public ItemExtensionType type;
        public IItemExtensionInfo info;
    }

    public abstract class ItemExtensionSOAbc : ScriptableObject { }

    public interface IItemExtensionInfo
    {
        
    }
}