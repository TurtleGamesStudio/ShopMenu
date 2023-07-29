using UnityEngine;

namespace TurtleGamesStudio.ItemSpace
{
    [CreateAssetMenu()]
    public class ItemData : ScriptableObject
    {
        [field: SerializeField] public ItemsNames Name { get; private set; }
        [field: SerializeField] public StatsData StatsData { get; private set; }

        public ItemData Copy()
        {
            ItemData copy = new ItemData();
            copy.Name = Name;
            copy.StatsData = StatsData;

            return copy;
        }
    }
}
