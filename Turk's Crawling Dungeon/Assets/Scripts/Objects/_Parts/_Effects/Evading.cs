using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Graphics;
using TCD.Objects.Parts.Talents;

namespace TCD.Objects.Parts.Effects
{
    public class Evading : Effect
    {
        private int level;

        public override string Name => "Evading";

        public override Sprite Icon => SpriteUtility.GetSprite("WeaveIcon");

        public Evading() : base()
        {

        }

        public Evading(int level) : this()
        {
            this.level = level;
        }

        public override EffectStacking Stacking => EffectStacking.None;

        public override string GetDescription() => $"Dodging increased by {Weave.GetEvasion(level)}%!";

        protected override void OnGetStat(GetStatEvent e)
        {
            GetStatEvent getStatEvent = e;
            if (getStatEvent.stat == Stat.Dodge)
                getStatEvent.level = getStatEvent.level + Weave.GetEvasion(level);
        }
    }
}
