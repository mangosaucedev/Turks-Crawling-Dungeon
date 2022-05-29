using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Zones.Utilities
{
    public static class PerlinNoiseExtensions
    {
        private enum LerpRules
        {
            Standard,
            UseMaxValue,
            UseMinValue
        }

        private static TGrid<float> a;
        private static TGrid<float> b;

        public static TGrid<float> LerpPerlinNoise(this TGrid<float> a, TGrid<float> b, float t)
        {
            PerlinNoiseExtensions.a = a;
            PerlinNoiseExtensions.b = b;
            return LerpPerlinNoise(t);
        }

        private static TGrid<float> LerpPerlinNoise(float t, LerpRules rules = LerpRules.Standard)
        {
            int width = Mathf.Min(a.width, b.width);
            int height = Mathf.Min(a.height, b.height);

            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                {
                    float aValue = a[x, y];
                    float bValue = b[x, y];
                    switch (rules)
                    {
                        default:
                            aValue = Mathf.Clamp01(Mathf.Lerp(aValue, bValue, t));
                            break;
                        case LerpRules.UseMaxValue:
                            aValue = Mathf.Clamp01(Mathf.Max(Mathf.Lerp(aValue, bValue, t), aValue, bValue));
                            break;
                        case LerpRules.UseMinValue:
                            aValue = Mathf.Clamp01(Mathf.Min(Mathf.Lerp(aValue, bValue, t), aValue, bValue));
                            break;
                    }
                    a[x, y] = aValue;
                }

            return a;
        }

        public static TGrid<float> LerpPerlinNoiseMax(this TGrid<float> a, TGrid<float> b, float t)
        {
            PerlinNoiseExtensions.a = a;
            PerlinNoiseExtensions.b = b;
            return LerpPerlinNoise(t, LerpRules.UseMaxValue);
        }

        public static TGrid<float> LerpPerlinNoiseMin(this TGrid<float> a, TGrid<float> b, float t)
        {
            PerlinNoiseExtensions.a = a;
            PerlinNoiseExtensions.b = b;
            return LerpPerlinNoise(t, LerpRules.UseMinValue);
        }

        public static TGrid<float> SmoothPerlinNoise(this TGrid<float> grid, int amount = 1, int factor = 1)
        {
            a = grid;

            for (int i = 0; i < amount; i++)
            {
                TGrid<float> copy = new TGrid<float>(grid.width, grid.height);
                grid.CopyTo(copy);
                b = copy;

                for (int x = 0; x < grid.width; x++)
                    for (int y = 0; y < grid.height; y++)
                        copy[x, y] = Smooth(x, y, factor);

                copy.CopyTo(grid);
            }

            return grid;
        }

        private static float Smooth(int x, int y, int factor)
        {
            float value = a.Get(x, y);
            int divisor = 1;
            for (int xOffset = -factor; xOffset <= factor; xOffset++)
                for (int yOffset = -factor; yOffset <= factor; yOffset++)
                {
                    int sX = x + xOffset;
                    int sY = y + yOffset;
                    if (!b.IsWithinBounds(sX, sY) || (sX == 0 && sY == 0))
                        continue;
                    value += a.Get(sX, sY);
                    divisor++;
                }
            return (float) value / divisor;
        }

        public static TGrid<float> AddPerlinNoise(this TGrid<float> a, TGrid<float> b)
        {
            for (int x = 0; x < a.width; x++)
                for (int y = 0; y < a.height; y++)
                    a[x, y] = Mathf.Clamp01(a[x, y] + b[x, y]);

            return a;
        }

        public static TGrid<float> SubtractPerlinNoise(this TGrid<float> a, TGrid<float> b)
        {
            for (int x = 0; x < a.width; x++)
                for (int y = 0; y < a.height; y++)
                    a[x, y] = Mathf.Clamp01(a[x, y] - b[x, y]);  

            return a;
        }

        public static TGrid<float> MultiplyPerlinNoise(this TGrid<float> a, float amount)
        {
            for (int x = 0; x < a.width; x++)
                for (int y = 0; y < a.height; y++)
                    a[x, y] = Mathf.Clamp01(a[x, y] * amount);

            return a;
        }

        public static TGrid<float> RoundPerlinNoise(this TGrid<float> a)
        {
            for (int x = 0; x < a.width; x++)
                for (int y = 0; y < a.height; y++)
                    a[x, y] = Mathf.Round(a[x, y]);

            return a;
        }

        public static TGrid<float> CeilPerlinNoise(this TGrid<float> a)
        {
            for (int x = 0; x < a.width; x++)
                for (int y = 0; y < a.height; y++)
                    a[x, y] = Mathf.Ceil(a[x, y]);

            return a;
        }
    }
}
