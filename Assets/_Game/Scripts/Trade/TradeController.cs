using System;
using Stocks;
using Stocks.Inventories;
using Stocks.TestData;
using UnityEngine;
using Zenject;

namespace Trade
{
    //gonna refactor it tomorrow I am too tired to write good code
    public class TradeController : MonoBehaviour
    {
        [Inject] private TestInventoryDataBuilder builder;
        [SerializeField] private TestInventoryData leftData;
        [SerializeField] private TestInventoryData rightData;

        [SerializeField] private InventoryView leftView;
        [SerializeField] private InventoryView rightView;
        
        private InventoryModel leftInventory;
        private InventoryModel rightInventory;

        private void Start()
        {
            leftInventory = MakeInventory(leftData);
            leftView.SetInventory(leftInventory);
            leftView.OnItemClickedEvent += OnClicked;
            
            rightInventory = MakeInventory(rightData);
            rightView.SetInventory(rightInventory);
            rightView.OnItemClickedEvent += OnClicked;
        }

        private void OnClicked(InventoryView view, ItemModel item)
        {
            var callerInv = view == leftView ? leftInventory : rightInventory;
            var otherInv = view == leftView ? rightInventory : leftInventory;
            callerInv.Remove(item);
            otherInv.TryAdd(item);
        }

        private InventoryModel MakeInventory(TestInventoryData data)
        {
            var inv = new InventoryModel();
            foreach (var itemModel in builder.Build(data)) 
                inv.TryAdd(itemModel);
            return inv;
        }
    }
} 