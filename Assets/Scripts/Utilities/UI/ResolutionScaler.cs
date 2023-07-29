using UnityEngine;

namespace TurtleGamesStudio.Utilities.UI
{
    public static class ResolutionScaler
    {
        private static Vector2Int _originalResolution;

        private static float _orginalRatio;
        private static float _currentRatio;
        public static float RatioOfRatios { get; private set; }

        public static float HeightMultiplier { get; private set; }
        public static float WidthMultiplier { get; private set; }

        public static float MinimalMultiplier { get; private set; }

        static ResolutionScaler()
        {
            Init();
        }

        private static void Init()
        {
            _originalResolution = new Vector2Int(720, 1560);
            CalculateOrginalRatio();
            Recalculate();
        }

        private static void CalculateOrginalRatio()
        {
            _orginalRatio = (float)_originalResolution.x / _originalResolution.y;
        }

        private static void CalculateCurrentRatio()
        {
            _currentRatio = (float)Screen.width / Screen.height;
        }

        private static void CalculateRatioOfRatios()
        {
            RatioOfRatios = _currentRatio / _orginalRatio;
        }

        private static void CalculateHeightMultiplier()
        {
            HeightMultiplier = (float)Screen.height / _originalResolution.y;
        }

        private static void CalculateWidthMultiplier()
        {
            WidthMultiplier = (float)Screen.width / _originalResolution.x;
        }

        private static void CalculateMinimalMultiplier()
        {
            if (HeightMultiplier < WidthMultiplier)
            {
                MinimalMultiplier = HeightMultiplier;
            }
            else
            {
                MinimalMultiplier = WidthMultiplier;
            }
        }

        public static void Recalculate()
        {
            CalculateCurrentRatio();
            CalculateRatioOfRatios();

            CalculateHeightMultiplier();
            CalculateWidthMultiplier();
            CalculateMinimalMultiplier();
        }

        public static Vector2 GetPosition(Vector2 oldPosition)
        {
            Vector2 newPosition = new Vector2(oldPosition.x * RatioOfRatios, oldPosition.y);
            return newPosition;
        }

        public static Vector3 SaveScaleProportion(Vector3 oldScale)
        {
            if (RatioOfRatios > 1)
            {
                return oldScale;
            }
            else
            {
                return oldScale * RatioOfRatios;
            }
        }

        public static Vector2 SaveRelativeWidth(Vector2 oldScale)
        {
            return new Vector2(oldScale.x * RatioOfRatios, oldScale.y);
        }

        public static Vector2 GetNewScale(Vector2 oldScale)
        {
            return oldScale * MinimalMultiplier;
        }

        public static Vector3 GetNewScale(Vector3 oldScale)
        {
            return oldScale * MinimalMultiplier;
        }

        public static Vector2 GetNewAnchoredPosition(Vector2 oldAncharedPosition)
        {
            return new Vector2(oldAncharedPosition.x * WidthMultiplier, oldAncharedPosition.y * HeightMultiplier);
        }
    }
}
