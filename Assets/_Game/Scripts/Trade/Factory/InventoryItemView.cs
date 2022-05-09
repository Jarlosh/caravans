using System;
using System.Collections.Generic;
using System.Linq;
using Stocks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Random = UnityEngine.Random;

namespace Trade.Factory
{
    public class InventoryItemView : MonoBehaviour, 
        IDisposable, IPoolable<Transform, ItemModel, ItemInfo, IMemoryPool>
    {
        [SerializeField] private Image iconImage;
        [SerializeField] Button button;
        
        IMemoryPool _pool;
        
        public ItemModel Item { get; private set; }

        public event Action<InventoryItemView> OnClickedEvent;

        public void SetItemData(ItemModel item, ItemInfo desc)
        {
            iconImage.sprite = desc.Icon;
            iconImage.color = desc.Color;
            Item = item;
        }

        public void Dispose()
        {
            _pool.Despawn(this);
            button.onClick.AddListener(OnClicked);
        }

        public void OnDespawned()
        {
            _pool = null;
            // transform.parent = null;
            Item = null;
        }
        
        private void OnClicked() => OnClickedEvent?.Invoke(this);

        public void OnSpawned(Transform parent, ItemModel item, ItemInfo desc, IMemoryPool pool)
        {
            transform.SetParent(parent);
            _pool = pool;
            SetItemData(item, desc);
            button.onClick.AddListener(OnClicked);
        }
        
        public class PoolFactory : PlaceholderFactory<Transform, ItemModel, ItemInfo, InventoryItemView>
        {
            public new InventoryItemView Create(Transform parent, ItemModel item, ItemInfo desc)
            {
                return base.Create(parent, item, desc);
            }
        }
    }
}