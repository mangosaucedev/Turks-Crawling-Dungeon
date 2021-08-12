using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TCD
{
    public class TilemapManager : MonoBehaviour
    {
        [SerializeField] protected Tilemap tilemap;

        public Tilemap Tilemap
        {
            get
            {
                if (!tilemap)
                    tilemap = GetComponent<Tilemap>();
                return tilemap;
            }
        }

        public TileBase this[int x, int y]
        {
            get => Get(x, y);
            set => Set(x, y, value);
        }

        public TileBase this[Vector2Int position]
        {
            get => Get(position);
            set => Set(position, value);
        }

        protected virtual void Awake()
        {
            if (!tilemap)
                tilemap = GetComponent<Tilemap>();
        }

        public void Set(Vector3Int position, TileBase tile) =>
            Set(position.x, position.y, tile);

        public void Set(Vector2Int position, TileBase tile) =>
            Set(position.x, position.y, tile);

        public void Set(int x, int y, TileBase tile)
        {
            Vector3Int position = new Vector3Int(x, y, 0);
            Tilemap.SetTile(position, tile);
        }

        public TileBase Get(Vector3Int position) =>
            Get(position.x, position.y);

        public TileBase Get(Vector2Int position) =>
            Get(position.x, position.y);

        public TileBase Get(int x, int y)
        {
            Vector3Int position = new Vector3Int(x, y, 0);
            return Tilemap.GetTile(position);
        }

        public bool HasTile(Vector3Int position) =>
            HasTile(position.x, position.y);

        public bool HasTile(Vector2Int position) =>
            HasTile(position.x, position.y);

        public bool HasTile(int x, int y)
        {
            Vector3Int position = new Vector3Int(x, y, 0);
            return Tilemap.HasTile(position);
        }
    }
}
