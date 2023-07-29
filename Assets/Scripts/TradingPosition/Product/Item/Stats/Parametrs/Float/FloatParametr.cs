using UnityEngine;
using System;

namespace TurtleGamesStudio.ItemSpace
{
    [Serializable]
    public struct FloatParametr
    {
        [field: SerializeField] public ParametrName.Float ParametrName { get; private set; }
        [field: SerializeField] public float Parametr { get; private set; }
    }
}
