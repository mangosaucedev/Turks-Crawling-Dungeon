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

        public float GetWeight()
        {
            GetWeightEvent e = LocalEvent.Get<GetWeightEvent>();
            e.weight = baseWeight;
            FireEvent(parent, e);
            return e.weight;
        }
    }
}
