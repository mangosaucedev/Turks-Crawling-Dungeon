using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts.Effects
{
    public abstract class Effect : Part
    {
        public int timeRemaining;

        public abstract EffectStacking Stacking { get; }

        public override bool HandleEvent<T>(T e)
        {
            if (e.Id == GetStatEvent.id)
                OnGetStat(e);
            return base.HandleEvent(e);
        }

        private void OnGetStat(LocalEvent e)
        {
            GetStatEvent getStatEvent = (GetStatEvent) e;
            OnGetStat(getStatEvent);
        }

        protected virtual void OnGetStat(GetStatEvent e)
        {

        }

        public abstract string GetDescription();
    }
}
