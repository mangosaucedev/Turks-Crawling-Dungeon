using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Parts.Effects;

namespace TCD.Objects.Parts
{
    public struct Injury : IInjury
    {
        public static readonly Injury defaultInjury = new Injury 
        { 
            severity = TimeInfo.TIME_PER_STANDARD_TURN * 10, 
            baseName = "Injury",
            baseDescription = "An injury."
        };

        public float severity;
        public string baseName;
        public string baseDescription;

        private Effect effect;

        private Injuries injuries;

        public Injuries Injuries
        {
            get => injuries;
            set => injuries = value;
        }

        public void OnAcquire()
        {
            if (!injuries || !injuries.parent.Parts.TryGet(out Effects.Effects effects))
                return;
            effects.AddEffect(effect = new Bleeding(1f, 1f), 99999);
        }

        public void OnHealed()
        {
            if (!injuries || effect == null || !injuries.parent.Parts.TryGet(out Effects.Effects effects))
                return;
            effects.RemoveEffect(effect);
        }

        public bool PassTime(int time)
        { 
            severity -= time;
            if (severity <= 0f)
                return true;
            return false;
        }

        public float GetSeverity() => severity;

        public string GetName() => baseName;

        public string GetDescription() => baseDescription;
    }
}
