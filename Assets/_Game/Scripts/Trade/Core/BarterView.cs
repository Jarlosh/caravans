using UnityEngine;

namespace _Game.Scripts.Trade.Core
{

    public class BarterView 
    {
        private IBarterModel _barterModel;
        
        public void SetModel(IBarterModel barterModel)
        {
            this._barterModel = barterModel;
            Subscribe();
        }

        private void Subscribe()
        {
            
        }
    }
}