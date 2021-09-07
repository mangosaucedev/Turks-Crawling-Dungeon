using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Zones.Environments;

namespace TCD.Zones
{
    public abstract class Chamber : IChamber
    {
        private int x;
        private int y; 
        private int width;
        private int height;
        private BoundsInt boundsInt;
        private HashSet<Vector2Int> occupiedPositions = new HashSet<Vector2Int>();
        private Environment environment;
        private TGrid<ChamberCellType> cells;
        private List<ChamberAnchor> anchors = new List<ChamberAnchor>();

        public int X
        {
            get => x;
            set => SetPosition(value, Y);
        }

        public int Y
        {
            get => y;
            set => SetPosition(X, value);
        }

        public Vector2Int Position
        {
            get => new Vector2Int(X, Y);
            set => SetPosition(value);
        }

        public int Width
        {
            get => width;
            set => width = value;
        }

        public int Height
        {
            get => height;
            set => height = value;
        }

        public BoundsInt BoundsInt
        {
            get => boundsInt;
            set => boundsInt = value;
        }

        public HashSet<Vector2Int> OccupiedPositions => occupiedPositions;

        public Environment Environment
        {
            get => environment;
            set => environment = value;
        }

        public TGrid<ChamberCellType> Cells => cells;

        public List<ChamberAnchor> Anchors => anchors;

        public Chamber(int width, int height)
        {
            Width = width;
            Height = height;         
            cells = new TGrid<ChamberCellType>(width, height);
            Generate();
            GenerateAnchors();
            BoundsInt = new BoundsInt(new Vector3Int(x, y, 0), new Vector3Int(width, height, 0));
        }

        public abstract void Generate();

        public void GenerateAnchors()
        {
            ChamberAnchorResolver chamberAnchorResolver = 
                new ChamberAnchorResolver(this);
            chamberAnchorResolver.ResolveAnchors();
        }

        public void SetPosition(Vector2Int position) =>
            SetPosition(position.x, position.y);

        public void SetPosition(int x, int y)
        {
            this.x = x;
            this.y = y;
            BoundsInt = new BoundsInt(new Vector3Int(x, y, 0), new Vector3Int(width, height, 0));
        }
    }
}
