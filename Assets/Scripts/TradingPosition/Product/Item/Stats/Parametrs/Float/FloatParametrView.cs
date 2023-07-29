using UnityEngine;
using TMPro;
using Zenject;
using UnityEngine.UI;

namespace TurtleGamesStudio.ItemSpace
{
    public class FloatParametrView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _value;
        [SerializeField] private Image _icon;

        private ParametrIconBase _parametrIconBase;

        [Inject]
        private void Construct(ParametrIconBase parametrIconBase)
        {
            _parametrIconBase = parametrIconBase;
        }

        public void Init(FloatParametr floatParametr)
        {
            transform.localPosition = Vector3.zero;
            _value.text = floatParametr.Parametr.ToString();
            _icon.sprite = _parametrIconBase.GetIcon(floatParametr.ParametrName);
        }
    }
}
