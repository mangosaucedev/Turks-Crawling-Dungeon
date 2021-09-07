using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TCD.Objects;
using TCD.Zones.Utilities;

namespace TCD.Zones
{
    public class ZoneBuilder : ZoneGeneratorMachine
    {
        private GroundTilemapManager groundTilemap;
        private PerlinNoiseGrid surfaceMap;

        private GroundTilemapManager GroundTilemap
        {
            get
            {
                if (!groundTilemap)
                    groundTilemap = ServiceLocator.Get<GroundTilemapManager>();
                return groundTilemap;
            }
        }

        private TGrid<ChamberCellType> Cells => Zone.CellTypes;

        private ZoneTerrain ZoneTerrain => Zone.ZoneTerrain;

        public override IEnumerator Generate()
        {
            surfaceMap = new PerlinNoiseGrid(Width, Height, 1);
            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                {
                    BuildCell(x, y);
                }
            yield break;
        }

        private void BuildCell(int x, int y)
        {
            ChamberCellType cell = Cells[x, y];
            if (cell <= ChamberCellType.Border)
                return;
            PlaceFloor(x, y);
            if (cell == ChamberCellType.Wall)
                PlaceWall(x, y);
            if (cell == ChamberCellType.Door)
                PlaceDoor(x, y);
        }

        private void PlaceFloor(int x, int y)
        {
            float surface = surfaceMap[x, y];
            string floor = ZoneTerrain.GetFloor(surface);
            TileBase tile = Assets.Get<TileBase>(floor);
            GroundTilemap.Set(x, y, tile);
        }

        private void PlaceWall(int x, int y)
        {
            float surface = surfaceMap[x, y];
            string wall = ZoneTerrain.GetWall(surface);
            BuildObject(x, y, wall);
        }

        private void BuildObject(int x, int y, string name)
        { 
            Vector2Int position = new Vector2Int(x, y);
            ObjectFactory.BuildFromBlueprint(name, position);
        }

        private void PlaceDoor(int x, int y) => BuildObject(x, y, "Door");
    }
}
