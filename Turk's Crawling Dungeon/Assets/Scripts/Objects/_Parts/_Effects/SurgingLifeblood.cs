using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Graphics;
using TCD.Objects.Parts.Talents;

namespace TCD.Objects.Parts.Effects
{
    public class SurgingLifeblood : Effect
    {
        private int level;

        public override string Name => "Surging Lifeblood";

        public override Sprite Icon => SpriteUtility.GetSprite("LifebloodIcon");

        public SurgingLifeblood() : base()
        {

        }

        public SurgingLifeblood(int level) : this()
        {
            this.level = level;
        }

        public override EffectStacking Stacking => EffectStacking.None;

        public override string GetDescription() => $"Hitpoint regeneration increased by {Lifeblood.GetHealingMultiplier(level) * 100f}%!";

        public override bool HandleEvent<T>(T e)
        {
            if (e is GetResourceRegenEvent)
                OnGetResourceRegen((GetResourceRegenEvent)(LocalEvent)e);
            return base.HandleEvent(e);
        }

        private void OnGetResourceRegen(GetResourceRegenEvent e)
        {
            if (e.resource == Resource.Hitpoints)
                e.amount *= Lifeblood.GetHealingMultiplier(level);
        }
    }
}
