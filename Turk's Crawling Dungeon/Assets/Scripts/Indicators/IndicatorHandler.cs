using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;

namespace TCD.Indicators
{
    public static class IndicatorHandler
    {
        private const int INFINITE_RANGE = -1;

        private static Indicator currentIndicator;
        private static GameObject currentIndicatorObject;

        public static void ShowAreaOfEffectIndicator(int radius, BaseObject target = null)
        {
            if (!target)
            {

            }
        }

        public static void ShowAreaOfEffectIndicator(int radius, Vector2Int position)
        {
            
        }

        public static void ShowPlayerToCursorIndicator(bool blockedByObstacles = true, int range = INFINITE_RANGE)
        {

        }

        private static Indicator CreateIndicator(string name)
        {
            return null;
        }
    }
}
