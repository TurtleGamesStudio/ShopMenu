using System.Collections.Generic;
using UnityEngine;
using System;

namespace TurtleGamesStudio.ItemSpace
{
    [CreateAssetMenu()]
    public class ParametrIconBase : ScriptableObject
    {
        [SerializeField] private FloatParametrIcon[] _floatParametrs;
        [SerializeField] private AbilityParametrIcon[] _abilityParametrs;

        public IReadOnlyList<FloatParametrIcon> FloatParametrs => _floatParametrs;

        public Sprite GetIcon(ParametrName.Float parametr)
        {
            foreach (var parametrIconDependency in _floatParametrs)
                if (parametrIconDependency.ParametrName == parametr)
                    return parametrIconDependency.Sprite;

            throw new NotImplementedException($"{parametr}");
        }

        public Sprite GetIcon(ParametrName.Ability parametr)
        {
            foreach (var parametrIconDependency in _abilityParametrs)
                if (parametrIconDependency.ParametrName == parametr)
                    return parametrIconDependency.Sprite;

            throw new NotImplementedException($"{parametr}");
        }
    }
}
