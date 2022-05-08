using System;
using System.Linq;
using Stocks;
using UnityEngine;
using Zenject;

namespace _Game.Scripts
{
    public class TestClass : IInitializable
    {
        [Inject] private IItemConfigManager itemMan;


        public void Initialize()
        {
            var values = itemMan.Values.ToArray();
            Array.Sort(values, (v1, v2) => v1.ID.CompareTo(v2.ID));
            var strs = values.Select(v => v.ToString());
            
            Debug.Log(string.Join("\n", strs));
        }
    }
}