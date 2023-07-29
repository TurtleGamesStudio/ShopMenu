using UnityEngine;
using System;

namespace TurtleGamesStudio.ItemSpace
{
    [Serializable]
    public class AbilityParametrIcon
    {
        [field: SerializeField] public ParametrName.Ability ParametrName { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
    }
}
