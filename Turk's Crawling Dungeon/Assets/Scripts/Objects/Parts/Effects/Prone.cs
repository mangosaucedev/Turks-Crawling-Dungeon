using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts.Effects
{
    public class Prone : Effect
    {
        private const int DODGE_PENALTY = 40;
        private const int PHYSICAL_SAVE_PENALTY = 20;
        private const int MOVE_SPEED_PENALTY_PERCENT = 50;

        public override string Name => "Prone";

        public override Sprite Icon => Assets.Get<Sprite>("ProneIcon");

        public override EffectStacking Stacking => EffectStacking.None;

        public override string GetDescription() => $"Characters that are prone suffer a " +
            $"-{DODGE_PENALTY} penalty to their dodge, a -{PHYSICAL_SAVE_PENALTY} penalty to " +
            $"physical saving throws, and have their movement speed reduced by {MOVE_SPEED_PENALTY_PERCENT}%.";

        protected override void OnGetStat(GetStatEvent e)
        {
            base.OnGetStat(e);
            if (e.stat == Stat.Dodge)
                e.level -= DODGE_PENALTY;
            if (e.stat == Stat.PhysicalSave)
                e.level -= PHYSICAL_SAVE_PENALTY;
            if (e.stat == Stat.MoveCost)
                e.level = e.level * 3 / 2;
        }
    }
}
