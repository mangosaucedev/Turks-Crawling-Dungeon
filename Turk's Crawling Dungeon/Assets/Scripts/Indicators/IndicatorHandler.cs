using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;

namespace TCD.Indicators
{
    public static class IndicatorHandler
    {
        private static Indicator currentIndicator;
        private static GameObject currentIndicatorObject;

        public static void ShowIndicator(string indicatorName, int range, bool blockedByObstacles = false)
        {
            HideIndicator();
            CreateIndicator(indicatorName);
            currentIndicator.range = range;
            currentIndicator.blockedByObstacles = blockedByObstacles;
        }

        private static void CreateIndicator(string name)
        {
            GameObject prefab = Assets.Get<GameObject>(name);
            currentIndicatorObject = Object.Instantiate(prefab, ParentManager.Temp);
            currentIndicator = currentIndicatorObject.GetComponent<Indicator>();
        }

        public static void HideIndicator()
        {
            if (currentIndicatorObject)
                Object.Destroy(currentIndicatorObject);
            currentIndicator = null;
            currentIndicatorObject = null;
        }
    }
}
