using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Zenject;

namespace TurtleGamesStudio.Finance
{
    public class PriceView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _value;
        [SerializeField] private Image _icon;

        private Price _price;
        private WalletInitializer _walletInitializer;

        [Inject]
        public void Construct(WalletInitializer walletInitializer)
        {
            _walletInitializer = walletInitializer;
        }

        private void OnDisable()
        {
            _price.Changed -= OnChanged;
            _price.Setted -= OnChanged;
        }

        public void Init(Price price)
        {
            transform.localPosition = Vector3.zero;
            _price = price;
            _icon.sprite = _walletInitializer.GetIcon(price.Currency);
            _value.text = price.Value.ToString();
            _price.Changed += OnChanged;
            _price.Setted += OnChanged;
        }

        private void OnChanged(int value)
        {
            _value.text = value.ToString();
        }
    }
}
