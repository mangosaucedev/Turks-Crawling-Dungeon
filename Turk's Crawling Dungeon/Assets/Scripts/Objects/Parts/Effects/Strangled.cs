using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts.Effects
{
    public class Strangled : Effect
    {
        public override string Name => "Strangled";

        public override Sprite Icon => Assets.Get<Sprite>("PinnedIcon");

        public override EffectStacking Stacking => EffectStacking.None;

        public override string GetDescription() => $"Strangled characters cannot move or breathe!";

        public override bool HandleEvent<T>(T e)
        {
            if (e.Id == BeforeMoveEvent.id)
                return false;
            return base.HandleEvent(e);
        }
    }
}
