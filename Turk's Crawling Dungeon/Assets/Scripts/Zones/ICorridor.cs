using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Zones
{
    public interface ICorridor : IFeature
    {
        HashSet<Vector2Int> Cells { get; }

        void Generate();
    }
}
