using System.Collections.Generic;
using UnityEngine;
using System;

namespace TurtleGamesStudio.Finance
{
    [CreateAssetMenu(fileName = "ResourceIcons", menuName = "ResourceIcons")]
    public class ResourceIcons : ScriptableObject
    {
        [SerializeField] private ResourceIcon[] _icons;

        public IReadOnlyList<ResourceIcon> Icons => _icons;

        public Sprite GetIcon(Currency currency)
        {
            foreach (var icon in _icons)
            {
                if (icon.Currency == currency)
                    return icon.Icon;
            }

            throw new NotImplementedException($"{currency}");
        }
    }
}
