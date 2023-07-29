using TMPro;
using UnityEngine;
using Zenject;

namespace TurtleGamesStudio.ShopSpace
{
    public class TradingPositionView : MonoBehaviour
    {
        [SerializeField] private ProductView _productView;
        [SerializeField] private TMP_Text _count;

        private TradingPosition _tradingPosition;

        [Inject]
        private void Construct()
        {

        }

        private void OnDisable()
        {
            _tradingPosition.ProductCountChanged -= OnCountChanged;
        }

        public void Init(TradingPosition tradingPosition)
        {
            _tradingPosition = tradingPosition;
            transform.localPosition = Vector3.zero;
            _count.text = tradingPosition.Quantity.ToString();
            _productView.Init(tradingPosition.Product);
            _tradingPosition.ProductCountChanged += OnCountChanged;
        }

        private void OnCountChanged(TradingPosition _)
        {
            _count.text = _tradingPosition.Quantity.ToString();
        }
    }
}
