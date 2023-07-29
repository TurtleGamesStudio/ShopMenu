using TurtleGamesStudio.Finance;
using UnityEngine;
using TurtleGamesStudio.ItemSpace;
using TurtleGamesStudio.ShopSpace;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private WalletInitializer _walletInitializer;
    [SerializeField] private WalletViewSpawner _walletViews;
    [SerializeField] private ShopSpawner _shopSpawner;
    [SerializeField] private ItemSpawner _itemSpawner;
    [SerializeField] private MoneyCheat[] _moneyCheats;

    private void Start()
    {
        _walletInitializer.Init();
        _walletViews.Init();

        foreach (MoneyCheat moneyCheat in _moneyCheats)
            moneyCheat.Init();

        _shopSpawner.Show();
        _itemSpawner.Init(_shopSpawner);
    }
}
