using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts.Effects
{
    [Serializable]
    public class Pinned : Effect
    {
        public override string Name => "Pinned";

        public override Sprite Icon => Assets.Get<Sprite>("PinnedIcon");

        public override EffectStacking Stacking => EffectStacking.None;

        public override string GetDescription() => $"Pinned characters cannot move!";

        public override bool HandleEvent<T>(T e)
        {
            if (e.Id == BeforeMoveEvent.id)
                return false;
            return base.HandleEvent(e);
        }
    }
}
