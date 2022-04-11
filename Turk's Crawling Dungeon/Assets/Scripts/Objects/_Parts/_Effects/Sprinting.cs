using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Parts.Talents;

namespace TCD.Objects.Parts.Effects
{
    public class Sprinting : Effect
    {
        private int level;

        public override string Name => "Sprinting";

        public override Sprite Icon => Assets.Get<Sprite>("SprintIcon");

        public override EffectStacking Stacking => EffectStacking.None;

        public Sprinting(int level) : base()
        {
            this.level = level;
        }

        public override string GetDescription() => $"Moving {(Sprint.GetSprintSpeedMultiplier(level) - 1f) * 100f}% faster!";

        protected override void OnGetStat(GetStatEvent e)
        {
            GetStatEvent getStatEvent = e;
            float multiplier = 1f / Sprint.GetSprintSpeedMultiplier(level);
            if (getStatEvent.stat == Stat.MoveCost)
                getStatEvent.level = Mathf.CeilToInt(getStatEvent.level * multiplier);
        }
    }
}
