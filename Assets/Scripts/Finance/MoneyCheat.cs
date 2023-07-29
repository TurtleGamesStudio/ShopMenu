using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TurtleGamesStudio.Finance
{
    public class MoneyCheat : MonoBehaviour
    {
        [SerializeField] private Currency _currency;
        [SerializeField] private int _amount = 100;
        [SerializeField] private Button _button;

        private Wallet _wallet;
        private WalletInitializer _walletInitializer;

        [Inject]
        public void Construct(WalletInitializer walletInitializer)
        {
            _walletInitializer = walletInitializer;
            // _wallet = _walletInitializer.GetWallet(_currency);
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(Add);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(Add);
        }

        public void Init()
        {
            _wallet = _walletInitializer.GetWallet(_currency);
        }

        private void Add()
        {
            _wallet.PutIn(_amount);
        }
    }
}
