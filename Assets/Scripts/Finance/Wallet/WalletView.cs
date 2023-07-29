using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Zenject;

namespace TurtleGamesStudio.Finance
{
    public class WalletView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _value;

        private Currency _currency;
        private int _lastShowedValue;
        private Wallet _wallet;

        private WalletInitializer _walletInitializer;

        public Currency Currency => _currency;

        [Inject]
        private void Construct(WalletInitializer walletInitializer)
        {
            _walletInitializer = walletInitializer;
        }

        private void OnDisable()
        {
            _wallet.BalanceChanged -= OnBalanceChanged;
        }

        public void Init(Currency currency)
        {
            transform.localPosition = Vector3.zero;
            _currency = currency;
            _wallet = _walletInitializer.GetWallet(currency);
            Sprite icon = _walletInitializer.GetIcon(currency);

            _icon.sprite = icon;
            _lastShowedValue = _wallet.Value;
            _value.text = _lastShowedValue.ToString();
            _wallet.BalanceChanged += OnBalanceChanged;
        }

        private void OnBalanceChanged(int target)
        {
            _lastShowedValue = target;
            _value.text = _lastShowedValue.ToString();
        }
    }
}