using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    [ContainsGameStatics]
    public static class ScoreHandler 
    {
        [GameStatic(0)]
        public static long rawScore;

        [GameStatic(1)]
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
