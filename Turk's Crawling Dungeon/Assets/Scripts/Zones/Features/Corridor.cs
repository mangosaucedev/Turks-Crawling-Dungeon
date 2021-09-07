using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Zones.Environments;

namespace TCD.Zones
{
    public abstract class Corridor : ICorridor
    {
        private HashSet<Vector2Int> cells = new HashSet<Vector2Int>();

        protected ChamberAnchor start;
        protected ChamberAnchor end;
        protected int x;
        protected int y;
        protected int width;
        private BoundsInt boundsInt;
        private Environment environment;

        private IZone Zone => CurrentZoneInfo.zone;

        private IZoneParams ZoneParams => Zone.ZoneParams;

        private int MinCorridorWidth => ZoneParams.MinCorridorWidth;

        private int MaxCorridorWidth => ZoneParams.MaxCorridorWidth;

        public HashSet<Vector2Int> Cells => cells;

        public BoundsInt BoundsInt
        {
            get => boundsInt;
            set => boundsInt = value;
        }

        public HashSet<Vector2Int> OccupiedPositions => cells;

        public Environment Environment
        {
            get => environment;
            set => environment = value;
        }

        public Corridor(ChamberAnchor start, ChamberAnchor end)
        {
            this.start = start;
            this.end = end;
            width = Random.Range(MinCorridorWidth, MaxCorridorWidth + 1);
            Generate();
            CalculateBounds();
        }

        public abstract void Generate();

        private void CalculateBounds()
        {
            int xMin = 9999;
            int xMax = 0;
            int yMin = 9999;
            int yMax = 0;
            foreach (Vector2Int pos in OccupiedPositions)
            {
                if (pos.x < xMin)
                    xMin = pos.x;
                if (pos.x > xMax)
                    xMax = pos.x;
                if (pos.y < yMin)
                    yMin = pos.y;
                if (pos.y > yMax)
                    yMax = pos.y;
            }
            int width = xMax - xMin;
            int height = yMax - yMin;
            Vector3Int position = new Vector3Int(x, y, 0);
            Vector3Int size = new Vector3Int(width, height, 0);
            BoundsInt = new BoundsInt(position, size);
        }
    }
}
