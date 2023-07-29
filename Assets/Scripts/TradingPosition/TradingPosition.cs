using System.Collections.Generic;
using TurtleGamesStudio.Finance;
using System;
using TurtleGamesStudio.ItemSpace;

namespace TurtleGamesStudio.ShopSpace
{
    public class TradingPosition
    {
        public Product Product { get; private set; }
        public int Quantity { get; private set; }

        public event Action<TradingPosition> ProductCountChanged;

        public TradingPosition(ItemData item, IReadOnlyList<PriceData> pricies, int quantity, WalletInitializer walletInitializer)
        {
            Product = new Product(item, pricies, walletInitializer);
            Quantity = quantity;

            Product.Purchased += OnPurchased;
        }

        public TradingPosition(TradingPositionData tradingPositionData, WalletInitializer walletInitializer)
            : this(tradingPositionData.Item, tradingPositionData.Price, tradingPositionData.Quantity, walletInitializer)
        { }

        public TradingPositionData GetTradingPositionData()
        {
            TradingPositionData tradingPositionData = new TradingPositionData();
            tradingPositionData.Item = Product.Item;
            tradingPositionData.Price = Product.GetPriceDatas();
            tradingPositionData.Quantity = Quantity;
            return tradingPositionData;
        }

        private void OnPurchased(Product _)
        {
            Quantity--;
            ProductCountChanged?.Invoke(this);
        }

        ~TradingPosition()
        {
            Product.Purchased -= OnPurchased;
        }
    }
}
