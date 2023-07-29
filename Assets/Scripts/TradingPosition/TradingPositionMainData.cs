using System;
using System.Collections.Generic;
using TurtleGamesStudio.ItemSpace;

namespace TurtleGamesStudio.ShopSpace
{
    [Serializable]
    public class TradingPositionMainData
    {
        public ItemsNames Name;
        public int Quantity;

        public static TradingPositionMainData Get(ItemsNames name, IEnumerable<TradingPositionMainData> datas)
        {
            foreach (TradingPositionMainData data in datas)
                if (data.Name == name)
                    return data;

            return null;
        }
    }
}
