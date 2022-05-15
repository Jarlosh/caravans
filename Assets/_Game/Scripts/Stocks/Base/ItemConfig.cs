using System.Collections.Generic;
using UnityEngine;

namespace Stocks
{
    [CreateAssetMenu(menuName = "SO/ItemConfig", fileName = "ItemConfig", order = 0)]
    public class ItemConfig : ScriptableObject
    {
        [SerializeField] private List<ItemInfoSO> soInfo;

        public List<ItemInfoSO> Infos
        {
            get => soInfo;
            set => soInfo = value;
        }
    }
}