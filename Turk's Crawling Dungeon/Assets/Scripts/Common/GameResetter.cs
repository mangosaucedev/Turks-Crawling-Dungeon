using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Zones;

namespace TCD
{
    public static class GameResetter
    {
        public static void ResetGame()
        {
            ResetGameStatics();
            ScoreHandler.rawScore = 0;
            ScoreHandler.level = 1;
            ZoneResetter.ResetZone(true);
        }

        private static void ResetGameStatics()
        {

        }
    }
}
