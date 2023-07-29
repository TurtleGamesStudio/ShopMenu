using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace TurtleGamesStudio.Finance
{
    public class WalletViewSpawner : MonoBehaviour
    {
        [SerializeField] private List<Currency> _currencies;
        [SerializeField] private Transform _container;

        private List<WalletView> _views;
        private WalletViewFactory _walletViewFactory;

        [Inject]
        private void Construct(WalletViewFactory walletViewFactory)
        {
            _walletViewFactory = walletViewFactory;
        }

        public void Init()
        {
            _views = new List<WalletView>();

            foreach (Currency currency in _currencies)
            {
                WalletView walletView = _walletViewFactory.Create();
                walletView.transform.parent = _container;
                walletView.transform.localScale = Vector3.one;
                walletView.Init(currency);
                _views.Add(walletView);
            }
        }
    }
}