using TurtleGamesStudio.Finance;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using TurtleGamesStudio.ItemSpace;

namespace TurtleGamesStudio.ShopSpace
{
    public class ProductView : MonoBehaviour
    {
        [SerializeField] private ItemView _itemView;
        [SerializeField] private Button _purchase;
        [SerializeField] private Transform _container;

        private PriceViewFactory _priceViewFactory;
        private Product _product;

        [Inject]
        private void Construct(PriceViewFactory priceViewFactory)
        {
            _priceViewFactory = priceViewFactory;
        }

        private void OnEnable()
        {
            _purchase.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _purchase.onClick.RemoveListener(OnClick);
            _product.PurchaseSettedAvailable -= SetButtonInteractable;
            _product.PurchaseSettedUnavailable -= SetButtonUninteractable;
        }

        public void Init(Product product)
        {
            _product = product;
            _itemView.Init(product.Item);

            foreach (var price in product.Pricies)
            {
                PriceView priceView = _priceViewFactory.Create();
                priceView.transform.parent = _container;
                priceView.transform.localScale = Vector3.one;
                priceView.Init(price);
            }

            _product.PurchaseSettedAvailable += SetButtonInteractable;
            _product.PurchaseSettedUnavailable += SetButtonUninteractable;

            if (_product.CanPurchase)
                SetButtonInteractable();
            else
                SetButtonUninteractable();
        }

        private void OnClick()
        {
            _product.Purchase();
        }

        private void SetButtonInteractable()
        {
            _purchase.interactable = true;
        }

        private void SetButtonUninteractable()
        {
            _purchase.interactable = false;
        }
    }
}
