using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Attacks;

namespace TCD.Objects.Parts
{
    [Serializable]
    public class PlayerUnarmed : NaturalAttack
    {
        public override string Name => "Player Unarmed";

        protected override void OnGetAttacks(GetAttacksEvent e)
        {
            base.OnGetAttacks(e);
            Attack attack0 = AttackFactory.BuildFromBlueprint("UnarmedClaw");
            attack0.weapon = parent;
            e.AddAttack(attack0, 0.2f);
        }
    }
}
