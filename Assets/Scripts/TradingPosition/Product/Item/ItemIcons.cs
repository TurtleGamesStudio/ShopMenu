using System;
using System.Collections.Generic;
using UnityEngine;

namespace TurtleGamesStudio.ItemSpace
{
    [CreateAssetMenu()]
    public class ItemIcons : ScriptableObject
    {
        [SerializeField] private ItemIconDependency[] _icons;

        public IReadOnlyList<ItemIconDependency> Icons => _icons;

        public Sprite GetIcon(ItemsNames name)
        {
            foreach (var icon in _icons)
            {
                if (icon.Name == name)
                    return icon.Sprite;
            }

            throw new NotImplementedException($"{name}");
        }
    }
}