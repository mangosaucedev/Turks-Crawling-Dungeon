using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts.Effects
{
    public class Bleeding : Effect
    {
        public float damage;
        public float score;

        public override string Name => "Bleeding";

        public override Sprite Icon => Assets.Get<Sprite>("BleedingIcon");

        public override EffectStacking Stacking => EffectStacking.StackDoNotRefreshCooldown;

        public Bleeding(float damage, float score)
        {
            this.damage = damage;
            this.score = score;
        }

        protected override void OnAfterTurnTick(AfterTurnTickEvent e)
        {
            base.OnAfterTurnTick(e);
            /*
            if (effects.parent.parts.TryGet(out Resources resources))
            {
                float multiplier = e.timeElapsed / TimeInfo.TIME_PER_STANDARD_TURN;
                float damageThisTurn = -damage * multiplier;
                resources.ModifyResource(Resource.Hitpoints, -damage);
            }
            */
        }

        public override string GetDescription() => $"Bleeding does nothing right now.";
    }
}
