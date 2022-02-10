using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Juice
{
    public static class CombatJuiceHandler
    {
        private static JuiceManager JuiceManager => ServiceLocator.Get<JuiceManager>();

        public static void Punch(BaseObject attacker, Vector2Int direction)
        {
            Punch punch = new Punch(attacker, direction);
            JuiceManager.AddAnimation(punch);
        }
    }
}
