using System;
using System.Collections.Generic;
using TurtleGamesStudio.Finance;
using UnityEngine;
using TurtleGamesStudio.ItemSpace;

namespace TurtleGamesStudio.ShopSpace
{
    public class Product : MonoBehaviour
    {
        private List<Price> _prices;
        private WalletInitializer _walletInitializer;
        private List<PriceWalletPair> _priceWalletComparers;
        private List<Wallet> _wallets;

        public event Action<Product> Purchased;
        public event Action PurchaseSettedAvailable;
        public event Action PurchaseSettedUnavailable;

        public ItemData Item { get; private set; }
        public bool CanPurchase { get; private set; }

        public IReadOnlyList<Price> Pricies => _prices;

        public Product(ItemData item, IReadOnlyList<PriceData> pricies, WalletInitializer walletInitializer)
        {
            Item = item;
            _prices = new List<Price>(pricies.Count);
            _wallets = new List<Wallet>(pricies.Count);
            _priceWalletComparers = new List<PriceWalletPair>(pricies.Count);
            _walletInitializer = walletInitializer;

            foreach (PriceData priceData in pricies)
            {
                Price price = new Price(priceData);
                _prices.Add(price);

                Wallet wallet = _walletInitializer.GetWallet(priceData.Currency);
                _wallets.Add(wallet);

                PriceWalletPair priceWalletComparer = new PriceWalletPair(price, wallet);
                _priceWalletComparers.Add(priceWalletComparer);
            }

            foreach (Wallet wallet in _wallets)
                wallet.BalanceChanged += OnBalanceChanged;

            foreach (Price price in Pricies)
                price.Changed += OnPriceChanged;

            CanPurchase = CheckPurchaseAbility();
        }

        public void Purchase()
        {
            foreach (PriceWalletPair priceWalletPair in _priceWalletComparers)
                priceWalletPair.Withdraw();

            Purchased?.Invoke(this);
        }

        public bool CheckPurchaseAbility()
        {
            foreach (PriceWalletPair priceWalletComparer in _priceWalletComparers)
            {
                if (priceWalletComparer.IsResourcesEnough() == false)
                    return false;
            }

            return true;
        }

        public List<PriceData> GetPriceDatas()
        {
            List<PriceData> priceDatas = new List<PriceData>();

            foreach (Price price in Pricies)
            {
                PriceData priceData = new PriceData();
                priceData.Currency = price.Currency;
                priceData.Value = price.Value;
                priceDatas.Add(priceData);
            }

            return priceDatas;
        }

        private void OnBalanceChanged(int _)
        {
            UpdatePurchaseStatus();
        }

        private void OnPriceChanged(int _)
        {
            UpdatePurchaseStatus();
        }

        private void UpdatePurchaseStatus()
        {
            bool canPurchase = CheckPurchaseAbility();

            if (CanPurchase != canPurchase)
            {
                CanPurchase = canPurchase;

                if (CanPurchase)
                    PurchaseSettedAvailable.Invoke();
                else
                    PurchaseSettedUnavailable.Invoke();
            }
        }

        ~Product()
        {
            foreach (Wallet wallet in _wallets)
                wallet.BalanceChanged -= OnBalanceChanged;

            foreach (Price price in Pricies)
                price.Changed -= OnPriceChanged;
        }
    }
}
