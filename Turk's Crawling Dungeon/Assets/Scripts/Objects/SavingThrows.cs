using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Parts;

namespace TCD.Objects
{
    public static class SavingThrows 
    {
        public static bool MakeSavingThrow(BaseObject attacker, BaseObject defender, Stat attackingStat, Stat defendingStat)
        {
            if (!attacker.parts.TryGet(out Stats attackerStats) || !defender.parts.TryGet(out Stats defenderStats))
                return true;
            float attackingRoll = attackerStats.RollStat(attackingStat); 
            float defendingRoll = defenderStats.RollStat(defendingStat);
            return defendingRoll > attackingRoll;
        }
    }
}
