using TurtleGamesStudio.Finance;
using TurtleGamesStudio.Utilities.SaveServices;
using UnityEngine;
using Zenject;
using TurtleGamesStudio.ItemSpace;
using TurtleGamesStudio.ShopSpace;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private WalletInitializer _walletInitializer;
    [SerializeField] private ParametrIconBase _parametrIconBase;
    [SerializeField] private ItemIcons _itemIcons;
    [SerializeField] private ItemBase _itemBase;

    [Header("Templates")]
    [SerializeField] private WalletView _walletView;
    [SerializeField] private TradingPositionView _shopView;
    [SerializeField] private PriceView _priceView;
    [SerializeField] private FloatParametrView _floatParametrView;
    [SerializeField] private AbilityParametrView _abilityParametrView;

    public override void InstallBindings()
    {
        Container.Bind<ISaveService>().To<BinaryFormatterSaver>().FromNew().AsSingle().NonLazy();
        Container.BindInstance(_walletInitializer).AsSingle().NonLazy();
        Container.BindInstance(_parametrIconBase).AsSingle().NonLazy();
        Container.BindInstance(_itemIcons).AsSingle().NonLazy();
        Container.BindInstance(_itemBase).AsSingle().NonLazy();

        Container.BindFactory<WalletView, WalletViewFactory>().
            FromComponentInNewPrefab(_walletView);
        Container.BindFactory<TradingPositionView, TradingPositionViewFactory>().
            FromComponentInNewPrefab(_shopView);
        Container.BindFactory<PriceView, PriceViewFactory>().
            FromComponentInNewPrefab(_priceView);
        Container.BindFactory<FloatParametrView, FloatParametrViewFactory>().
           FromComponentInNewPrefab(_floatParametrView);
        Container.BindFactory<AbilityParametrView, AbilityParametrViewFactory>().
          FromComponentInNewPrefab(_abilityParametrView);
    }
}
