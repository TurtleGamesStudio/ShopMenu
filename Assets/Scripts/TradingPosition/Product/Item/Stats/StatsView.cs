using UnityEngine;
using Zenject;

namespace TurtleGamesStudio.ItemSpace
{
    public class StatsView : MonoBehaviour
    {
        [SerializeField] private Transform _container;

        private StatsData _statsData;
        private FloatParametrViewFactory _floatParametrViewFactory;
        private AbilityParametrViewFactory _abilityParametrViewFactory;

        [Inject]
        private void Construct(FloatParametrViewFactory floatParametrViewFactory, AbilityParametrViewFactory abilityParametrViewFactory)
        {
            _floatParametrViewFactory = floatParametrViewFactory;
            _abilityParametrViewFactory = abilityParametrViewFactory;
        }

        public void Init(StatsData statsData)
        {
            _statsData = statsData;

            foreach (FloatParametr floatParametr in _statsData.FloatParametrs)
            {
                FloatParametrView statView = _floatParametrViewFactory.Create();
                statView.transform.parent = _container;
                statView.transform.localScale = Vector3.one;
                statView.Init(floatParametr);
            }

            foreach (var parametr in _statsData.AbilityParametrs)
            {
                AbilityParametrView statView = _abilityParametrViewFactory.Create();
                statView.transform.parent = _container;
                statView.transform.localScale = Vector3.one;
                statView.Init(parametr);
            }
        }
    }
}
