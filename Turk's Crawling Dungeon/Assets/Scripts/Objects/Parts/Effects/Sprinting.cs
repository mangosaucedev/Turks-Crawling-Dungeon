using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TCD.Objects.Parts.Effects
{
    public class Sprinting : Effect
    {
        public override string Name => "Sprinting";

        public override Sprite Icon => Assets.Get<Sprite>("SprintIcon");

        public override EffectStacking Stacking => EffectStacking.None;

        public override string GetDescription() => $"Sprinting characters move 100% faster!";

        public override bool HandleEvent<T>(T e)
        {
            if (e.Id == GetStatEvent.id)
                OnGetStat(e);
            return base.HandleEvent(e);
        }

        private void OnGetStat(LocalEvent e)
        {
            GetStatEvent getStatEvent = (GetStatEvent) e;
            if (getStatEvent.stat == Stat.MoveCost)
                getStatEvent.level = Mathf.CeilToInt(getStatEvent.level * 0.5f);
        }
    }
}
