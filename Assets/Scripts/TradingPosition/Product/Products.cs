using System.Collections.Generic;
using UnityEngine;

namespace TurtleGamesStudio.ShopSpace
{
    [CreateAssetMenu()]
    public class Products : ScriptableObject
    {
        [SerializeField] private List<ProductData> _products;

        public IReadOnlyList<ProductData> List => _products;
    }
}
