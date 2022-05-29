using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Attacks;
using TCD.Texts;

namespace TCD.Objects.Parts
{
    public class AttackExhaustion : Part
    {
        private const float BASE_STAMINA = 4.4f;
        private const float STAMINA_PER_POUND = 0.6f;

        public override string Name => "Attack Exhaustion";

        public override bool HandleEvent<T>(T e)
        {
            if (e.Id == BeforeAttackEvent.id)
                return OnBeforeAttack((BeforeAttackEvent) (LocalEvent) e);
            return base.HandleEvent(e);
        }

        private bool OnBeforeAttack(BeforeAttackEvent e)
        {
            if (e.obj != parent || !parent.Parts.TryGet(out Resources resources))
                return true;
            
            float cost = GetStaminaCost(e);
            if (resources.GetResource(Resource.Stamina) < cost)
            {
                if (e.obj == PlayerInfo.currentPlayer)
                    FloatingTextHandler.Draw(e.obj.transform.position, "Too exhausted!", Color.red);
                return !e.SetResult("failed due to exhaustion");
            }

            return resources.ModifyResource(Resource.Stamina, -cost);
        }

        private float GetStaminaCost(BeforeAttackEvent e)
        {
            float cost = BASE_STAMINA;

            Attack attack = e.attack;
            if (attack.weapon != null && attack.weapon != e.obj && attack.weapon.Parts.TryGet(out PhysicsSim physics))
                cost += physics.GetWeight() * STAMINA_PER_POUND;

            return cost;
        }
    }
}
