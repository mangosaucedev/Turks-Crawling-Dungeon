using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Attacks
{
    public static class AttackFactory 
    {
        public static Attack BuildFromBlueprint(string blueprintName)
        {
            Attack blueprint = Assets.Get<Attack>(blueprintName);
            return BuildFromBlueprint(blueprint);
        }

        public static Attack BuildFromBlueprint(Attack blueprint)
        {
            Attack attack = (Attack) blueprint.Clone();
            attack.damageType = Assets.Get<DamageType>(attack.damageTypeName);
            attack.attackType = (AttackType) Enum.Parse(typeof(AttackType), attack.attackTypeName);
            return attack;
        }
        
    }
}
