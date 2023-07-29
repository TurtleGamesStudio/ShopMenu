using UnityEngine;
using System;

namespace TurtleGamesStudio.ItemSpace
{
    [Serializable]
    public struct ItemNameDependency
    {
        [field: SerializeField] public ItemsNames Name { get; private set; }
        [field: SerializeField] public Item Item { get; private set; }
    }
}
