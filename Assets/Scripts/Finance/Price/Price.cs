using System;

namespace TurtleGamesStudio.Finance
{
    [Serializable]
    public class Price
    {
        private Currency _currency;
        private int _value;

        public event Action<int> Changed;
        public event Action<int> Setted;

        public int Value => _value;
        public Currency Currency => _currency;

        public Price(Currency currency, int value)
        {
            _currency = currency;
            _value = value;
        }

        public Price(PriceData priceData) : this(priceData.Currency, priceData.Value)
        { }

        public void Decrease(int value)
        {
            if (value < 0)
            {
                throw new ArgumentException($"{nameof(value)} must be non negative.");
            }

            if (value > _value)
            {
                throw new ArgumentException($"{nameof(value)} must be not more than price.");
            }

            _value -= value;

            Changed?.Invoke(_value);
        }

        public PriceData GetPriceData()
        {
            PriceData priceData = new PriceData();
            priceData.Currency = _currency;
            priceData.Value = _value;
            return priceData;
        }
    }
}