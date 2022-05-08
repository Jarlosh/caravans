using System;
using System.Collections.Generic;
using UnityEngine;

namespace Stocks
{
    [CreateAssetMenu(menuName = "SO/Items/ItemInfoSO", fileName = "ItemInfoSO", order = 0)]
    public class ItemInfoSO : ScriptableObject
    {
        public string Name;
        public int ID;
        public Sprite Icon;
        public Color Color;
        
        public List<ItemExtensionSORef> Extensions;

        public override string ToString()
        {
            return $"[{ID}] {Name}";
        }
    }
    
    [Serializable]
    public class ItemInfo
    {
        public string Name;
        public int ID;
        public Sprite Icon;
        public Color Color;

        public List<ItemExtensionInfoRef> Extensions;

        public ItemInfo(ItemInfoSO infoSO, List<ItemExtensionInfoRef> extensions)
        {
            Name = infoSO.Name;
            ID = infoSO.ID;
            Color = infoSO.Color;
            Icon = infoSO.Icon;
            Extensions = extensions;
        }

        public override string ToString()
        {
            return $"[{ID}] {Name}";
        }
    }
}