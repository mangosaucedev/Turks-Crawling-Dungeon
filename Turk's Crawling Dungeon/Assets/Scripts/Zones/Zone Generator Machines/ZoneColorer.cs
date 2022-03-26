using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TCD.Objects;
using TCD.Objects.Parts;
using TCD.Zones.Utilities;

namespace TCD.Zones
{
    public class ZoneColorer : ZoneGeneratorMachine
    {
        private PerlinNoiseGrid redInfluence;
        private PerlinNoiseGrid greenInfluence;
        private PerlinNoiseGrid blueInfluence;
        private PerlinNoiseGrid whiteInfluence;

        public override string LoadMessage => "Coloring zone...";

        public override IEnumerator Generate()
        {
            redInfluence = GetRandomPerlinNoiseGrid();
            greenInfluence = GetRandomPerlinNoiseGrid();
            blueInfluence = GetRandomPerlinNoiseGrid();
            whiteInfluence = GetRandomPerlinNoiseGrid();

            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                {
                    Color color = GetColor(x, y);
                    ColorCell(x, y, color);
                }  
            yield break;
        }

        private PerlinNoiseGrid GetRandomPerlinNoiseGrid()
        {
            float scale = Random.Range(0.1f, 0.5f);
            return new PerlinNoiseGrid(Width, Height, scale);
        }

        private Color GetColor(int x, int y)
        {
            float red = Mathf.Lerp(redInfluence[x, y], 0.6f, 0.35f);
            float green = Mathf.Lerp(greenInfluence[x, y], 0.4f, 0.35f);
            float blue = Mathf.Lerp(blueInfluence[x, y], 0.0f, 0.5f);
            Color color = new Color(red, green, blue);
            float white = whiteInfluence[x, y] / 2f;
            return Color.Lerp(color, Color.white, white);
        }

        private void ColorCell(int x, int y, Color color)
        {
            TilemapManager manager = ServiceLocator.Get<GroundTilemapManager>();
            Tilemap tilemap = manager.Tilemap;
            Vector3Int cellPosition = new Vector3Int(x, y, 0);
            Color tileColor = new Color(1 - color.r, 1 - color.g, 1 - color.b);
            tileColor = Color.Lerp(tileColor, Color.white, 0.25f);
            tilemap.SetTileFlags(cellPosition, TileFlags.None);
            tilemap.SetColor(cellPosition, tileColor);

            GameGrid grid = CurrentZoneInfo.grid;
            Cell cell = grid[x, y];
            foreach (BaseObject obj in cell.objects)
            {
                if (obj.Parts.TryGet(out Render render))
                    render.AddColorLayer("Zone", color);
            }
        }
    }
}
