using System.Collections.Generic;
using UnityEngine;
using System;

namespace TurtleGamesStudio.ItemSpace
{
    [Serializable]
    public struct StatsData
    {
        [SerializeField] private FloatParametr[] _floatParametrs;
        [SerializeField] private AbilityParametr[] _abilityParametrs;

        public IReadOnlyList<FloatParametr> FloatParametrs => _floatParametrs;
        public IReadOnlyList<AbilityParametr> AbilityParametrs => _abilityParametrs;
    }
}
