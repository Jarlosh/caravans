using System;
using System.Collections.Generic;
using System.Linq;
using Stocks;
using Stocks.ItemHandle;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace _Game.Scripts
{
    public class TestClass : IInitializable
    {
        [Inject] private IItemConfigManager itemMan;
        [Inject] private ItemPool.Factory pool;

        public void Initialize()
        {
            var values = itemMan.Values.ToArray();
            Array.Sort(values, (v1, v2) => v1.ID.CompareTo(v2.ID));
            var strs = values.Select(v => v.ToString());
            
            Debug.Log(string.Join("\n", strs));

            PoolTest();
        }

        private void PoolTest()
        {
            var saved = new SortedList<ItemModel, int>(Comparer<ItemModel>.Create((m1, m2) => m1.ID.CompareTo(m2.ID)));
            for (int i = 0; i < 100; i++)
            {
                var count = Random.Range(0, 100);
                using (var b = new DisposeBlock())
                {
                    var items = Enumerable
                        .Range(0, count)
                        .Select(i => pool.Create(0, 1))
                        .ToList();

                    if (items.Any(saved.ContainsKey))
                        throw new Exception("FAIL");

                    var rem = Random.Range(0, count + 1);
                    for (int j = 0; j < rem; j++)
                    {
                        var ir = Random.Range(0, items.Count);
                        var ite = items[ir];
                        items.RemoveAt(ir);
                        saved.Add(ite, ite.ItemID);
                    }

                    b.AddRange(items);
                }
            }
        }
    }
}