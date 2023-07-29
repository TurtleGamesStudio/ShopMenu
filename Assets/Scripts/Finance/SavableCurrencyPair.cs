using System;

namespace TurtleGamesStudio.Finance
{
    [Serializable]
    public struct SavableCurrencyPair
    {
        public Currency Currency;
        public bool ShouldSave;
    }
}
