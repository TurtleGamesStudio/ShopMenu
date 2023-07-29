using System;

namespace TurtleGamesStudio.Finance
{
    public class Wallet
    {
        private Currency _currency;
        private int _value;

        public event Action<int> BalanceChanged;
        public event Action<int> Setted;

        public const int MaxValue = int.MaxValue;

        public int Value => _value;
        public Currency Currency => _currency;

        public Wallet(Currency currency, int value = 0)
        {
            _currency = currency;
            _value = value;
        }

        public void PutIn(int value)
        {
            if (value < 0)
            {
                throw new ArgumentException($"{nameof(value)} must be non negative", nameof(value));
            }

            _value += value;
            BalanceChanged?.Invoke(_value);
        }

        public void Withdraw(int value)
        {
            if (value < 0)
            {
                throw new ArgumentException($"{nameof(value)} must be non negative", nameof(value));
            }

            if (value > _value)
            {
                throw new ArgumentException($"You try withdraw more than {nameof(Wallet)} have", nameof(value));
            }

            _value -= value;
            BalanceChanged?.Invoke(_value);
        }
    }
}