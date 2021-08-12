using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public static class ScoreHandler 
    {
        [GameStatic]
        public static long rawScore;

        [GameStatic]
        public static int level = 1;

        public static void ModifyScore(int value)
        {
            rawScore += value;
        }

        public static long GetScore()
        {
            return rawScore;
        }
    }
}
