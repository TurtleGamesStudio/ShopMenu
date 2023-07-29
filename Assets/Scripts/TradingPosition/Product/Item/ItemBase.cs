using System;
using System.Collections.Generic;
using UnityEngine;

namespace TurtleGamesStudio.ItemSpace
{
    [CreateAssetMenu()]
    public class ItemBase : ScriptableObject
    {
        [SerializeField] private ItemNameDependency[] _items;

        public IReadOnlyList<ItemNameDependency> Items => _items;

        public Item GetItem(ItemsNames name)
        {
            foreach (var item in _items)
            {
                if (item.Name == name)
                    return item.Item;
            }

            throw new NotImplementedException($"{name}");
        }
    }
}
