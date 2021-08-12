using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Zones
{
    public static class FeatureBoundsChecker
    {
        public static bool OverlapsOtherFeature(Vector3Int position, IFeature feature)
        {
            IZone zone = CurrentZoneInfo.zone;
            foreach (IFeature checkFeature in zone.Features)
            {
                if (feature == checkFeature)
                    continue;
                if (checkFeature.BoundsInt.Contains(position))
                    return true;
            }
            return false;
        }
    }
}
