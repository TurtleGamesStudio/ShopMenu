using UnityEngine;
using System;

namespace TurtleGamesStudio.ItemSpace
{
    [Serializable]
    public struct ItemIconDependency
    {
        [field: SerializeField] public ItemsNames Name { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
    }
}
