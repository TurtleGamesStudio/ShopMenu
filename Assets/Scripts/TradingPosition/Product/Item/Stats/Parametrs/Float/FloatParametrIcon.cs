using UnityEngine;
using System;

namespace TurtleGamesStudio.ItemSpace
{
    [Serializable]
    public class FloatParametrIcon
    {
        [field: SerializeField] public ParametrName.Float ParametrName { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
    }
}
