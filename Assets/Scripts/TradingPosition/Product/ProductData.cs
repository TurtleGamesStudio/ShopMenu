using TurtleGamesStudio.Finance;
using System;
using TurtleGamesStudio.ItemSpace;

namespace TurtleGamesStudio.ShopSpace
{
    [Serializable]
    public struct ProductData
    {
        public ItemData Item;
        public PriceData Price;
    }
}
