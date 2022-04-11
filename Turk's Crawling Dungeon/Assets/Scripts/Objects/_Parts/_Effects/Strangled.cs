using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts.Effects
{
    [Serializable]
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
            if (e is GetResourceRegenEvent)
                OnGetResourceRegen((GetResourceRegenEvent)(LocalEvent)e);
            return base.HandleEvent(e);
        }

        private void OnGetResourceRegen(GetResourceRegenEvent e)
        {
            if (e.resource == Resource.Oxygen)
                e.amount = 0f;
        }
    }
}
