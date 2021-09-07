using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Zones.Utilities
{
    public class PerlinNoiseGrid : SeededGrid<float>
    {
        private float scale;

        public PerlinNoiseGrid(int width, int height, float scale) : base(width, height)
        {
            this.scale = scale;
            Fill();
        }

        protected override void Fill()
        {
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                { 
                    FillCell(x, y);
                }
        }

        protected override void FillCell(int x, int y)
        {
            float xCoord = (float) x / (width * scale);
            float yCoord = (float) y / (height * scale);
            float value = Mathf.PerlinNoise(xCoord, yCoord);
            Set(x, y, value);
        }
    }
}
