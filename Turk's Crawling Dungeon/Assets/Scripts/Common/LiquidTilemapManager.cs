using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TCD
{
    public class LiquidTilemapManager : TilemapManager
    {
        public TileBase liquidTile;

        protected override void Awake()
        {
            base.Awake();
            if (!liquidTile)
                liquidTile = Assets.Get<TileBase>("Tiled Liquid");
        }

        public void Draw(Vector2Int position, Color color)
        {
            Vector3Int tilePosition = (Vector3Int) position;
            Tilemap.SetTile(tilePosition, liquidTile);
            Tilemap.SetTileFlags(tilePosition, TileFlags.None);
            Tilemap.SetColor(tilePosition, color);
        }

        public void Erase(Vector2Int position)
        {
            Vector3Int tilePosition = (Vector3Int) position;
            Tilemap.SetTile(tilePosition, null);
        }
    }
}
