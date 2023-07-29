using UnityEngine;
using System;

namespace TurtleGamesStudio.ItemSpace
{
    [Serializable]
    public struct AbilityParametr
    {
        [field: SerializeField] public ParametrName.Ability ParametrName { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
    }
}
