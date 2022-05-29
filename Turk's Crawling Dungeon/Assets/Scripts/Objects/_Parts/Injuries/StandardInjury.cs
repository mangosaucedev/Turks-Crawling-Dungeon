using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Attacks;

namespace TCD.Objects.Parts
{
    public abstract class StandardInjury : IInjury
    {
        protected float severity;
        protected int timeToHeal;

        private Injuries injuries;

        public Injuries Injuries
        {
            get => injuries;
            set => injuries = value;
        }

        public StandardInjury(Attack attack)
        {
            LoadAttack(attack);
        }

        protected abstract void LoadAttack(Attack attack);

        public abstract void OnAcquire();

        public abstract void OnHealed();

        public bool PassTime(int time)
        {
            timeToHeal -= time;
            if (timeToHeal <= 0)
                return true; 
            return false;
        }

        public abstract float GetSeverity();

        public abstract string GetName();

        public abstract string GetDescription();
    }
}
