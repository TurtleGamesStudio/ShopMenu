using System.Collections.Generic;
using System.IO;
using System.Linq;
using TurtleGamesStudio.Utilities.SaveServices;
using UnityEngine;
using Zenject;

namespace TurtleGamesStudio.Finance
{
    public class WalletInitializer : MonoBehaviour
    {
        private const string WALLETS_KEY = "WALLETS";
        private const string EXTENSION = ".dat";

        [SerializeField] private SavableCurrencyPair[] _walletsSettings;
        [SerializeField] private ResourceIcons _resourceIcons;

        private List<Wallet> _wallets;
        private List<Wallet> _savableWallets;
        private bool _hasChanges = false;
        private string _path;

        [Inject] private ISaveService _saveService;

        private void OnDisable()
        {
            foreach (var wallet in _savableWallets)
                wallet.BalanceChanged -= OnBalancedChanged;
        }

        private void LateUpdate()
        {
            if (_hasChanges)
            {
                Save();
                _hasChanges = false;
            }
        }

        public void Init()
        {
            _path = Path.Combine(Application.persistentDataPath, WALLETS_KEY + EXTENSION);
            _wallets = new List<Wallet>(_walletsSettings.Length);
            _savableWallets = new List<Wallet>();

            if (_saveService.HasContainer(_path))
                Load();
            else
                CreateWallets();
        }

        public Wallet GetWallet(Currency currency)
        {
            return _wallets.FirstOrDefault(targetWallet => targetWallet.Currency == currency);
        }

        public Sprite GetIcon(Currency currency)
        {
            return _resourceIcons.GetIcon(currency);
        }

        private void Load()
        {
            PriceData[] walletDatas = _saveService.Load<PriceData[]>(_path);

            foreach (var walletData in walletDatas)
            {
                Wallet wallet;

                if (ShouldSave(walletData.Currency))
                {
                    wallet = new Wallet(walletData.Currency, walletData.Value);
                    AddToSavable(wallet);
                }
                else
                {
                    wallet = new Wallet(walletData.Currency);
                }

                _wallets.Add(wallet);
            }

            foreach (var savableCurrencyPair in _walletsSettings)
            {
                if (GetWallet(savableCurrencyPair.Currency) == null)
                    CreateWallet(savableCurrencyPair);
            }
        }

        private void AddToSavable(Wallet wallet)
        {
            wallet.BalanceChanged += OnBalancedChanged;
            _savableWallets.Add(wallet);
        }

        private bool ShouldSave(Currency currency)
        {
            bool shouldSave = false;

            foreach (SavableCurrencyPair savableCurrencyPair in _walletsSettings)
            {
                if (savableCurrencyPair.Currency == currency)
                    return savableCurrencyPair.ShouldSave;
            }

            return shouldSave;
        }

        private void CreateWallets()
        {
            _wallets = new List<Wallet>(_walletsSettings.Length);
            _savableWallets = new List<Wallet>();

            for (int i = 0; i < _walletsSettings.Length; i++)
            {
                SavableCurrencyPair walletsSetting = _walletsSettings[i];
                CreateWallet(walletsSetting);
            }
        }

        private void CreateWallet(SavableCurrencyPair walletsSetting)
        {
            Wallet wallet = new Wallet(walletsSetting.Currency);
            _wallets.Add(wallet);

            if (walletsSetting.ShouldSave)
                AddToSavable(wallet);
        }

        private void OnBalancedChanged(int _)
        {
            _hasChanges = true;
        }

        private void Save()
        {
            PriceData[] saveData = new PriceData[_savableWallets.Count];

            for (int i = 0; i < saveData.Length; i++)
            {
                PriceData walletData = new PriceData();
                walletData.Currency = _savableWallets[i].Currency;
                walletData.Value = _savableWallets[i].Value;
                saveData[i] = walletData;
            }

            _saveService.Save(_path, saveData);
        }
    }
}
