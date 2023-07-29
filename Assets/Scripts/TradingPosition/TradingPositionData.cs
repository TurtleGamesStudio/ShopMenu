using TurtleGamesStudio.Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using TurtleGamesStudio.ItemSpace;

namespace TurtleGamesStudio.ShopSpace
{
    [Serializable]
    public class TradingPositionData
    {
        public ItemData Item;
        public List<PriceData> Price;
        public int Quantity;

        public TradingPositionData Copy()
        {
            TradingPositionData copy = new TradingPositionData();
            copy.Item = Item.Copy();
            PriceData[] priceDatas = new PriceData[Price.Count];
            Price.CopyTo(priceDatas);
            copy.Price = priceDatas.ToList();
            copy.Quantity = Quantity;

            return copy;
        }
    }
}
