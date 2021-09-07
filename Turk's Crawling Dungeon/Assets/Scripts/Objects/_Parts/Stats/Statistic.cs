using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts
{
    [Serializable]
    public abstract class Statistic : Part
    {
        [SerializeField] private int baseLevel;

        public int BaseLevel
        {
            get => baseLevel;
            set => baseLevel = value;
        }

        protected abstract Stat Stat { get; }

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
            if (e.stat != Stat)
                return;
        }
    }
}
