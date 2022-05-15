using System;
using System.Collections.Generic;
using System.Linq;
using Stocks;
using Stocks.Inventories;
using UniRx;

namespace Trade
{
    public class InventoryController : IInventoryController
    {
        private const int MaxPerStack = 2;//int.MaxValue;
        public IInventoryModel Model { get; private set; }

        public InventoryController(IInventoryModel model)
        {
            Model = model;
        }
        
        //todo: implement
        public void RemoveCount(ItemModel item, int count)
        {
            throw new System.NotImplementedException();
        }

        public void DecreaseStack(ItemStack stack, int count)
        {
            if (!Model.Contains(stack)) 
                throw new Exception("Stack not found");

            stack.ChangeCount(-count);
            
            if (stack.Count.Value == 0)
                Model.Stacks.Remove(stack);
        }

        public void AddCount(ItemModel item, int count)
        {
            var stacks = Model.GetStacksOf(item).ToArray();

            foreach (var stack in stacks)
            {
                var toLoad = Math.Min(MaxPerStack - stack.Count.Value, count);
                count -= toLoad;
                stack.ChangeCount(+toLoad);
            }
            
            ExtendStacks(item, totalCount: count);
        }

        private void ExtendStacks(ItemModel item, int totalCount)
        {
            var fullStacksCount = totalCount / MaxPerStack;
            var remainder = totalCount % MaxPerStack;
            
            for (int i = 0; i < fullStacksCount; i++) 
                AddStack(item, MaxPerStack);
            
            if(remainder > 0)
                AddStack(item, remainder);
        }

        private void AddStack(ItemModel item, int count)
        {
            Model.Stacks.Add(new ItemStack(item, count));
        }

        public void Transfer(IInventoryController other, ItemStack stack, int count)
        {
            other.AddCount(stack.Item, count);
            DecreaseStack(stack, count);
        }
    }
}