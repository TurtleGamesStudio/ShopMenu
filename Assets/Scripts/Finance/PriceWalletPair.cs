namespace TurtleGamesStudio.Finance
{
    public class PriceWalletPair
    {
        private Price _price;
        private Wallet _wallet;

        public PriceWalletPair(Price price, Wallet wallet)
        {
            _price = price;
            _wallet = wallet;
        }

        public bool IsResourcesEnough(int productsCount = 1)
        {
            return _price.Value * productsCount <= _wallet.Value;
        }

        public void Withdraw()
        {
            _wallet.Withdraw(_price.Value);
        }
    }
}
