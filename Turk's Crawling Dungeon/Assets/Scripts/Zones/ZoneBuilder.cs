using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TCD.Objects;

namespace TCD.Zones
{
    public class ZoneBuilder : GeneratorObject
    {
        private GroundTilemapManager groundTilemap;

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

        public override IEnumerator Generate()
        {
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
            TileBase tile = Assets.Get<TileBase>("TileFloor");
            GroundTilemap.Set(x, y, tile);
        }

        private void PlaceWall(int x, int y) => BuildObject(x, y, "Wall");

        private void BuildObject(int x, int y, string name)
        { 
            Vector2Int position = new Vector2Int(x, y);
            ObjectFactory.BuildFromBlueprint(name, position);
        }

        private void PlaceDoor(int x, int y) => BuildObject(x, y, "Door");
    }
}
