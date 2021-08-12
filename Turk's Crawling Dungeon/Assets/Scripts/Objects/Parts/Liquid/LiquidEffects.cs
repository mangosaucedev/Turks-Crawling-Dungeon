using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts
{
    public static class LiquidEffects 
    {
        public static void Drink(string liquid, BaseObject drinker)
        {
            if (!drinker == PlayerInfo.currentPlayer)
                return;
            switch (liquid)
            {
                case "Acid":
                    DrinkAcid();
                    break;
                default:
                    DrinkWater();
                    break;
            }
        }

        private static void DrinkAcid()
        {
            MessageLog.Add("You just drank acid!");
        }

        private static void DrinkWater()
        {
            MessageLog.Add("You feel no odd effects.");
        }
    }
}
