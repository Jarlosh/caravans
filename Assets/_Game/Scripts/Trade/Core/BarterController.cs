namespace _Game.Scripts.Trade.Core
{
    public class BarterController
    {
        private BarterView _barterView;
        private IBarterModel _barterModel;

        public BarterController(IBarterModel barterModel, BarterView barterView)
        {
            this._barterModel = barterModel;
            this._barterView = barterView;
            barterView.SetModel(barterModel);
        }
        
        
    }
}