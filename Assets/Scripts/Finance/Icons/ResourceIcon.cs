using System;
using UnityEngine;

namespace TurtleGamesStudio.Finance
{
    [Serializable]
    public class ResourceIcon
    {
        [SerializeField] private Currency _currency;
        [SerializeField] private Sprite _icon;

        public Currency Currency => _currency;
        public Sprite Icon => _icon;
    }
}