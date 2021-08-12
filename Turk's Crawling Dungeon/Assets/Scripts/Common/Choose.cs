using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public static class Choose
    {
        public static T Random<T>(params T[] ts)
        {
            return ts[RandomInfo.Random.Next(0, ts.Length)];
        }

        public static T Random<T>(List<T> ts)
        {
            return ts[RandomInfo.Random.Next(0, ts.Count)];
        }
    }
}