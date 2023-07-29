using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurtleGamesStudio.Utilities.UI
{
    public class WindowResizer : MonoBehaviour
    {
        [SerializeField] private bool CanWindowResizeAtRuntime = false;

        private List<UIResizer> _uiResizers = new List<UIResizer>();

        private float _currentWidth;
        private float _currentHeight;

        public void Init()
        {
            _currentWidth = Screen.width;
            _currentHeight = Screen.height;

            if (CanWindowResizeAtRuntime)
                StartCoroutine(Updating());
        }

        public void Add(UIResizer uIResizer)
        {
            _uiResizers.Add(uIResizer);
        }

        private IEnumerator Updating()
        {
            while (true)
            {
                if (_currentHeight != Screen.height || _currentWidth != Screen.width)
                {
                    _currentWidth = Screen.width;
                    _currentHeight = Screen.height;
                    Resize();
                }

                yield return null;
            }
        }

        private void Resize()
        {
            ResolutionScaler.Recalculate();

            int lastElement = _uiResizers.Count - 1;

            for (int i = lastElement; i >= 0; i--)
            {
                UIResizer uiResizer = _uiResizers[i];

                if (uiResizer == null)
                {
                    _uiResizers.RemoveAt(i);
                }
                else
                {
                    uiResizer.Resize();
                    uiResizer.Recenter();
                }
            }
        }
    }
}
