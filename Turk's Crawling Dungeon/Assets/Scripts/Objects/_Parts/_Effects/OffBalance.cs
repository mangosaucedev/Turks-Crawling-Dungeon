using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts.Effects
{
    [Serializable]
    public class OffBalance : Effect
    {
        private const int DODGE_PENALTY = 10;
        private const int PHYSICAL_SAVE_PENALTY = 10;

        public override string Name => "Off-Balance";

        public override Sprite Icon => Assets.Get<Sprite>("TackleIcon");

        public override EffectStacking Stacking => EffectStacking.RefreshCooldown;

        public override string GetDescription() => $"Off-Balance characters suffer a " +
            $"-{DODGE_PENALTY} penalty to dodge and a -{PHYSICAL_SAVE_PENALTY} penalty to " +
            $"physical saving throws.";

        protected override void OnGetStat(GetStatEvent e)
        {
            base.OnGetStat(e);
            if (e.stat == Stat.Dodge)
                e.level -= DODGE_PENALTY;
            if (e.stat == Stat.PhysicalSave)
                e.level -= PHYSICAL_SAVE_PENALTY;
        }
    }
}
