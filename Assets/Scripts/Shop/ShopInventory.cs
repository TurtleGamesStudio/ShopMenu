using System.Collections.Generic;
using UnityEngine;

namespace TurtleGamesStudio.ShopSpace
{
    [CreateAssetMenu()]
    public class ShopInventory : ScriptableObject
    {
        [SerializeField] private List<TradingPositionData> _items;

        public IReadOnlyList<TradingPositionData> Items => _items;
    }
}