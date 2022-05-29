using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts
{
    [Serializable]
    public class PhysicsSim : Part
    {
        [SerializeField] private float baseWeight;

        public float BaseWeight
        {
            get => baseWeight;
            set => baseWeight = value;
        }

        public override string Name => "Physics";

        public override bool HandleEvent<T>(T e)
        {
            if (e.Id == GetDescriptionEvent.id)
                OnGetDescription((GetDescriptionEvent) (LocalEvent) e);
            return base.HandleEvent(e);
        }

        private void OnGetDescription(GetDescriptionEvent e)
        {
            e.AddToSuffix($"#{GetWeight()}");
        }

        public float GetWeight()
        {
            GetWeightEvent e = LocalEvent.Get<GetWeightEvent>();
            e.weight = baseWeight;
            FireEvent(parent, e);
            return e.weight;
        }
    }
}
