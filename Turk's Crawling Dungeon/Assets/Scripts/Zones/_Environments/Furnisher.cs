using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Zones.Environments
{
    public abstract class Furnisher
    {
        public float weight;
        public bool forced;
        public bool exclusive;
        public Environment environment;

        protected IFeature currentFeature;

        public virtual void Furnish(IFeature feature, int x, int y)
        {
            currentFeature = feature;
        }
    }
}
