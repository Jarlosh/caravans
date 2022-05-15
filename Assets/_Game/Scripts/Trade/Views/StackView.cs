using System;
using System.Collections.Generic;
using System.Linq;
using Scripts.Tools.UI;
using Stocks;
using Stocks.Inventories;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Trade.Factory
{
    public class StackView : MonoBehaviour, 
        IDisposable, IPoolable<Transform, ItemStack, ItemInfo, IMemoryPool>
    {
        [SerializeField] private Image iconImage;
        [SerializeField] private Button button;
        [SerializeField] private UiCounter count;
        private const int countDigits = 2;
        private IDisposable subscription;
        
        IMemoryPool _pool;
        
        public ItemStack Stack { get; private set; }

        public event Action<StackView> OnClickedEvent;

        public void SetItemData(ItemStack item, ItemInfo desc)
        {
            Stack = item;
            iconImage.sprite = desc.Icon;
            iconImage.color = desc.Color;

            subscription = Stack.Count
                .Subscribe(v => count.Value = v);
        }

        public void Dispose()
        {
            subscription?.Dispose();
            button.onClick.RemoveListener(OnClick);
            _pool.Despawn(this);
        }

        public void OnDespawned()
        {
            _pool = null;
            // transform.parent = null;
            Stack = null;
        }

        public void OnSpawned(Transform parent, ItemStack item, ItemInfo desc, IMemoryPool pool)
        {
            transform.SetParent(parent);
            _pool = pool;
            SetItemData(item, desc);

            button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            OnClickedEvent?.Invoke(this);
        }

        public class PoolFactory : PlaceholderFactory<Transform, ItemStack, ItemInfo, StackView>
        {
            public new StackView Create(Transform parent, ItemStack item, ItemInfo desc)
            {
                return base.Create(parent, item, desc);
            }
        }
    }
}