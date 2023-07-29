using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TurtleGamesStudio.ItemSpace
{
    public class ItemView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private StatsView _statsView;

        private ItemIcons _icons;

        [Inject]
        private void Construct(ItemIcons icons)
        {
            _icons = icons;
        }

        public void Init(ItemData item)
        {
            _statsView.Init(item.StatsData);
            _icon.sprite = _icons.GetIcon(item.Name);
        }
    }
}
