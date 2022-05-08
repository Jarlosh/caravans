using System;
using Stocks;
using UnityEngine;
using UnityEngine.UI;

namespace Trade
{
    public class InventoryItemView : MonoBehaviour
    {
        [SerializeField] private Image iconImage;
        [SerializeField] Button button;
        
        public ItemModel Item { get; private set; }

        public event Action<InventoryItemView> OnClickedEvent;

        private void Start()
        {
            button.onClick.AddListener(OnClicked);
        }
        
        private void OnDestroy()
        {
            button.onClick.AddListener(OnClicked);
        }

        private void OnClicked() => OnClickedEvent?.Invoke(this);

        public void SetItemData(ItemModel item, ItemInfo desc)
        {
            iconImage.sprite = desc.Icon;
            iconImage.color = desc.Color;
            this.Item = item;
        }
    }
}