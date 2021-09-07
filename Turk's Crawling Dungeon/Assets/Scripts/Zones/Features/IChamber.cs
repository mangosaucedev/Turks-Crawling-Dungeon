using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Zones
{
    public interface IChamber : IFeature
    {
        int X { get; set; }

        int Y { get; set; }

        Vector2Int Position { get; set; }

        int Width { get; set; }

        int Height { get; set; }

        TGrid<ChamberCellType> Cells { get; }

        List<ChamberAnchor> Anchors { get; }

        void Generate();

        void GenerateAnchors();

        void SetPosition(Vector2Int position);

        void SetPosition(int x, int y);
    }
}
