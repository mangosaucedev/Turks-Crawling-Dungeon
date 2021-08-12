using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Zones.Environments;

namespace TCD.Zones
{
    public interface IFeature
    {
        BoundsInt BoundsInt { get; set; }

        HashSet<Vector2Int> OccupiedPositions { get; }

        Environment Environment { get; set; }
    }
}
