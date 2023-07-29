using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TurtleGamesStudio.Utilities.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class UIResizer : MonoBehaviour
    {
        [SerializeField] private SizeSettings _sizeSetting;
        [SerializeField] private PivotSettings _pivotSettings;

        private RectTransform _rectTransform;
        private Vector2 _originalSize;
        private Vector2 _originalOffsetFromPivot;

        private static WindowResizer _instance;

        private void Awake()
        {
            Init();
            ResolutionScaler.Recalculate();
            Resize();
            Recenter();

            if (_instance == null)
            {
                _instance = new GameObject(nameof(WindowResizer), typeof(WindowResizer)).
                    GetComponent<WindowResizer>();
                _instance.Init();
            }

            _instance.Add(this);
        }

        public void Init()
        {
            _rectTransform = GetComponent<RectTransform>();
            _originalSize = _rectTransform.sizeDelta;
            _originalOffsetFromPivot = _rectTransform.anchoredPosition;
        }

        public void Resize()
        {
            switch (_sizeSetting)
            {
                case SizeSettings.SaveProportions:
                    _rectTransform.sizeDelta = _originalSize * ResolutionScaler.MinimalMultiplier;//ResolutionScaler.RatioOfRatios;
                    break;
                case SizeSettings.SaveRelativeWidth:
                    _rectTransform.sizeDelta = new Vector2(_originalSize.x * ResolutionScaler.WidthMultiplier,
                        _originalSize.y);
                    break;
                case SizeSettings.SaveRelativeHeight:
                    _rectTransform.sizeDelta = new Vector2(_originalSize.x,
                        _originalSize.y * ResolutionScaler.HeightMultiplier);
                    break;
                case SizeSettings.SaveRelativeWidthAndHeight:
                    _rectTransform.sizeDelta = new Vector2(_originalSize.x * ResolutionScaler.WidthMultiplier,
                       _originalSize.y * ResolutionScaler.HeightMultiplier);
                    break;
                case SizeSettings.SaveConstantSize:
                    break;
                default:
                    throw new NotImplementedException($"{_sizeSetting}");
            }
        }

        public void Recenter()
        {
            switch (_pivotSettings)
            {
                case PivotSettings.SaveRelativeDistance:
                    _rectTransform.anchoredPosition =
                        new Vector2(_originalOffsetFromPivot.x * ResolutionScaler.MinimalMultiplier,
                       _originalOffsetFromPivot.y * ResolutionScaler.MinimalMultiplier);
                    break;
                default:
                    throw new NotImplementedException($"{_sizeSetting}");
            }
        }

        public enum SizeSettings
        {
            SaveProportions = 0,
            SaveRelativeWidth = 1,
            SaveRelativeHeight = 2,
            SaveRelativeWidthAndHeight = 3,
            SaveConstantSize = 4,
        }

        public enum PivotSettings
        {
            SaveRelativeDistance = 0,
        }
    }
}
