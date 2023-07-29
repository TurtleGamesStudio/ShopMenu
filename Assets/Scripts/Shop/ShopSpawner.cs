using System;
using System.Collections.Generic;
using System.IO;
using TurtleGamesStudio.Finance;
using TurtleGamesStudio.Utilities.SaveServices;
using UnityEngine;
using Zenject;
using TurtleGamesStudio.ItemSpace;

namespace TurtleGamesStudio.ShopSpace
{
    public class ShopSpawner : MonoBehaviour
    {
        private const string SAVE_KEY = "Shop";
        private const string EXTENSION = ".dat";

        [SerializeField] private ShopInventory _inventory;
        [SerializeField] private Transform _container;

        private TradingPositionViewFactory _factory;
        private WalletInitializer _walletInitializer;
        private Dictionary<TradingPosition, TradingPositionView> _tradingViewModelDependencies;
        private List<TradingPositionMainData> _tradingPositionMainData;
        private string _path;

        public event Action<TradingPositionMainData> Purchased;

        [Inject] private ISaveService _saveService;

        [Inject]
        private void Construct(TradingPositionViewFactory factory, WalletInitializer walletInitializer)
        {
            _factory = factory;
            _walletInitializer = walletInitializer;
        }

        private void OnDisable()
        {
            foreach (var tradingPosition in _tradingViewModelDependencies.Keys)
                tradingPosition.ProductCountChanged -= OnProductCountChange;
        }

        public void Show()
        {
            _path = Path.Combine(Application.persistentDataPath, SAVE_KEY + EXTENSION);
            _tradingViewModelDependencies = new Dictionary<TradingPosition, TradingPositionView>();
            _tradingPositionMainData = new List<TradingPositionMainData>();

            if (_saveService.HasContainer(_path))
                Load();
            else
            {
                foreach (TradingPositionData data in _inventory.Items)
                    AddNewDataPosition(data);
            }
        }

        private TradingPosition CreateTradingPosition(TradingPositionData data)
        {
            TradingPosition tradingPosition = new TradingPosition(data, _walletInitializer);

            if (data.Quantity != 0)
            {
                TradingPositionView tradingPositionView = _factory.Create();
                tradingPositionView.transform.parent = _container;
                tradingPositionView.transform.localScale = Vector3.one;

                tradingPositionView.Init(tradingPosition);
                _tradingViewModelDependencies.Add(tradingPosition, tradingPositionView);
                tradingPosition.ProductCountChanged += OnProductCountChange;
            }

            return tradingPosition;
        }

        private void AddNewDataPosition(TradingPositionData data)
        {
            TradingPosition tradingPosition = CreateTradingPosition(data);
            TradingPositionMainData tradingPositionMainData = new TradingPositionMainData();
            tradingPositionMainData.Name = tradingPosition.Product.Item.Name;
            tradingPositionMainData.Quantity = tradingPosition.Quantity;
            _tradingPositionMainData.Add(tradingPositionMainData);
        }

        private void Load()
        {
            _tradingPositionMainData = _saveService.Load<List<TradingPositionMainData>>(_path);

            foreach (TradingPositionData data in _inventory.Items)
            {
                TradingPositionData dataCopy = data.Copy();
                TradingPosition tradingPosition;
                TradingPositionMainData tradingPositionMainData =
                    TradingPositionMainData.Get(dataCopy.Item.Name, _tradingPositionMainData);

                if (tradingPositionMainData != null)
                {
                    dataCopy.Quantity = tradingPositionMainData.Quantity;
                    tradingPosition = CreateTradingPosition(dataCopy);
                }
                else
                {
                    AddNewDataPosition(dataCopy);
                }
            }
        }

        private void OnProductCountChange(TradingPosition tradingPosition)
        {
            if (tradingPosition.Quantity == 0)
            {
                tradingPosition.ProductCountChanged -= OnProductCountChange;
                TradingPositionView tradingPositionView = _tradingViewModelDependencies[tradingPosition];
                Destroy(tradingPositionView.gameObject);
                _tradingViewModelDependencies.Remove(tradingPosition);
            }

            SendPurchaseInfo(tradingPosition.Product.Item.Name, tradingPosition.Quantity);

            Save(tradingPosition.Product.Item.Name, tradingPosition.Quantity);
        }

        private void Save(ItemsNames name, int quantity)
        {
            TradingPositionMainData changingData =
                  _tradingPositionMainData.Find(changingData => changingData.Name == name);

            changingData.Quantity = quantity;

            _saveService.Save(_path, _tradingPositionMainData);
        }

        private void SendPurchaseInfo(ItemsNames name, int newQuantity)
        {
            TradingPositionMainData purchasingData = new TradingPositionMainData();
            purchasingData.Name = name;
            TradingPositionMainData changingData =
                  _tradingPositionMainData.Find(changingData => changingData.Name == name);
            purchasingData.Quantity = changingData.Quantity - newQuantity;
            Purchased?.Invoke(purchasingData);
        }
    }
}
