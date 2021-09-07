using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts.Effects
{
    public class Swimming : Effect
    {
        public override string Name => "Swimming";

        public override Sprite Icon => Assets.Get<Sprite>("SprintIcon");

        public override Color Color => Assets.Get<Color>("TurkBlue");

        public override EffectStacking Stacking => EffectStacking.None;

        public override bool ShowFloatingText => false;

        public override string GetDescription() => $"Swimming through deep liquid makes " +
            $"movement twice as difficult.";

        protected override void OnGetStat(GetStatEvent e)
        {
            base.OnGetStat(e);
            if (e.stat == Stat.MoveCost)
                e.baseLevel = Mathf.CeilToInt(e.baseLevel * 2f);
        }
    }
}
