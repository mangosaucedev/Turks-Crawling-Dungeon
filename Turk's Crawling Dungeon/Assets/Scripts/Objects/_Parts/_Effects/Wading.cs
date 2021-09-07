using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts.Effects
{
    [Serializable]
    public class Wading : Effect
    {
        public override string Name => "Wading";

        public override Sprite Icon => Assets.Get<Sprite>("SprintIcon");

        public override Color Color => Assets.Get<Color>("TurkBlue");

        public override EffectStacking Stacking => EffectStacking.None;

        public override bool ShowFloatingText => false;

        public override string GetDescription() => $"Wading through deep liquid makes movement 50% more " +
            $"difficult.";

        protected override void OnGetStat(GetStatEvent e)
        {
            base.OnGetStat(e);
            if (e.stat == Stat.MoveCost)
                e.baseLevel = Mathf.CeilToInt(e.baseLevel * 1.5f);
        }
    }
}
