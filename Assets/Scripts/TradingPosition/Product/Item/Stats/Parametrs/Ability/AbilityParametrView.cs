using UnityEngine;
using TMPro;
using Zenject;
using UnityEngine.UI;

namespace TurtleGamesStudio.ItemSpace
{
    public class AbilityParametrView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _description;
        [SerializeField] private Image _icon;

        private ParametrIconBase _parametrIconBase;

        [Inject]
        private void Construct(ParametrIconBase parametrIconBase)
        {
            _parametrIconBase = parametrIconBase;
        }

        public void Init(AbilityParametr parametr)
        {
            transform.localPosition = Vector3.zero;
            _description.text = parametr.Description.ToString();
            _icon.sprite = _parametrIconBase.GetIcon(parametr.ParametrName);
        }
    }
}