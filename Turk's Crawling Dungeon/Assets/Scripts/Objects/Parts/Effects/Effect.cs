using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts.Effects
{
    public abstract class Effect : ILocalEventHandler
    {
        public Effects effects;
        public int time;
        public int timeRemaining;

        public abstract string Name { get; }

        public abstract Sprite Icon { get; }

        public abstract EffectStacking Stacking { get; }

        protected BaseObject EffectedObject => effects?.parent;

        public virtual bool FireEvent<T>(ILocalEventHandler target, T e) where T : LocalEvent => 
            target.HandleEvent(e);

        public virtual bool HandleEvent<T>(T e) where T : LocalEvent
        {
            if (e.Id == GetStatEvent.id)
                OnGetStat(e);
            return true;
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
