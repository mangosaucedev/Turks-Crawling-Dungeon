using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Parts;

namespace TCD.Objects.Attacks
{
    public static class DamageHandler 
    {
        public static float PerformReductionsOnDamage(float damage, DamageType damageType, BaseObject defender)
        {
            if (!defender.parts.TryGet(out Stats stats))
                return damage;
            int armor = stats.GetStatLevel(Stat.Armor);
            int mentalArmor = stats.GetStatLevel(Stat.MentalArmor);
            damage -= damageType.armorSoak * Mathf.Min(damage, armor);
            damage *= Mathf.Pow(1 - damageType.armorReduction, armor);
            damage -= damageType.psiSoak * Mathf.Min(damage, mentalArmor);
            damage *= Mathf.Pow(1 - damageType.psiReduction, mentalArmor);
            return damage;
        }

        public static float AddPhysicalPowerToDamage(float damage, BaseObject attacker)
        {
            if (!attacker.parts.TryGet(out Stats stats))
                return damage;
            int physicalPower = stats.GetStatLevel(Stat.PhysicalPower);
            return damage += Mathf.Pow(physicalPower, 0.85f);
        }

        public static float AddMentalPowerToDamage(float damage, BaseObject attacker)
        {
            if (!attacker.parts.TryGet(out Stats stats))
                return damage;
            int mentalPower = stats.GetStatLevel(Stat.MentalPower);
            return damage += Mathf.Pow(mentalPower, 0.85f);
        }
    }
}
