using System.Collections.Generic;
using System.IO;
using TurtleGamesStudio.ShopSpace;
using TurtleGamesStudio.Utilities.SaveServices;
using UnityEngine;
using Zenject;

namespace TurtleGamesStudio.ItemSpace
{
    public class ItemSpawner : MonoBehaviour
    {
        private const string SAVE_KEY = "Inventory";
        private const string EXTENSION = ".dat";

        [SerializeField] private Vector3 _startSpawnPosition;
        [SerializeField] private Vector3 _offset;

        private string _path;
        private List<TradingPositionMainData> _purchasedItemsDatas;
        private ShopSpawner _shopSpawner;
        private Vector3 _lastSpawnPosition;
        private ItemBase _itemBase;

        [Inject] private ISaveService _saveService;

        [Inject]
        private void Construct(ItemBase itemBase)
        {
            _itemBase = itemBase;
        }

        private void OnDisable()
        {
            _shopSpawner.Purchased -= OnPurchased;
        }

        public void Init(ShopSpawner shopSpawner)
        {
            _path = Path.Combine(Application.persistentDataPath, SAVE_KEY + EXTENSION);
            _shopSpawner = shopSpawner;
            _lastSpawnPosition = _startSpawnPosition;
            _shopSpawner.Purchased += OnPurchased;

            if (_saveService.HasContainer(_path))
                Load();
            else
                _purchasedItemsDatas = new List<TradingPositionMainData>();
        }

        private void Load()
        {
            _purchasedItemsDatas = _saveService.Load<List<TradingPositionMainData>>(_path);

            foreach (var item in _purchasedItemsDatas)
                SpawnGroup(item);
        }

        private void SpawnGroup(TradingPositionMainData tradingPositionMainData)
        {
            Item template = _itemBase.GetItem(tradingPositionMainData.Name);

            for (int i = 0; i < tradingPositionMainData.Quantity; i++)
                Spawn(template);
        }

        private void Spawn(Item template)
        {
            Item item = Instantiate(template);
            item.transform.parent = transform;
            item.transform.localScale = Vector3.one;
            _lastSpawnPosition += _offset;
            item.transform.position = _lastSpawnPosition;
        }

        private void OnPurchased(TradingPositionMainData info)
        {
            SpawnGroup(info);

            TradingPositionMainData changingData = TradingPositionMainData.Get(info.Name, _purchasedItemsDatas);

            if (changingData != null)
            {
                changingData.Quantity += info.Quantity;
            }
            else
            {
                TradingPositionMainData newData = new TradingPositionMainData();
                newData.Name = info.Name;
                newData.Quantity = info.Quantity;
                _purchasedItemsDatas.Add(newData);
            }

            Save();
        }

        private static TradingPositionMainData Get(ItemsNames name, IEnumerable<TradingPositionMainData> datas)
        {
            foreach (TradingPositionMainData data in datas)
                if (data.Name == name)
                    return data;

            return null;
        }

        private void Save()
        {
            _saveService.Save(_path, _purchasedItemsDatas);
        }
    }
}
