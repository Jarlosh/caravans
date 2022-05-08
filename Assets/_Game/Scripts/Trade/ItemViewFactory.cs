using System;
using System.Collections.Generic;
using Stocks;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Trade
{
    //TODO: reimplement with pool
    public class ItemViewFactory : MonoBehaviour
    {
        [Inject] private IItemConfigManager itemManager;
        [SerializeField] private GameObject itemPrefab;
        [SerializeField] private Transform parent;

        public IReadOnlyDictionary<ItemModel, InventoryItemView> Spawned => _spawned;
        private Dictionary<ItemModel, InventoryItemView> _spawned 
            = new Dictionary<ItemModel, InventoryItemView>();

        public event Action<InventoryItemView> OnItemClickedEvent;
        
        public InventoryItemView Spawn(ItemModel itemModel)
        {
            var view = Instantiate(itemPrefab, parent)
                .GetComponent<InventoryItemView>();
            var desc = itemManager[itemModel.ItemID];
            
            view.SetItemData(itemModel, desc);
            view.OnClickedEvent += OnItemClicked;
            _spawned.Add(itemModel, view);
            return view;
        }

        public void Destroy(InventoryItemView itemView)
        {
            itemView.OnClickedEvent -= OnItemClicked;
            _spawned.Remove(itemView.Item);
            Destroy(itemView.gameObject);
        }

        private void OnItemClicked(InventoryItemView view)
        {
            OnItemClickedEvent?.Invoke(view);
        }
    }
}