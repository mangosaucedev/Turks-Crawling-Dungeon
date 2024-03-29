using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Attacks
{
    [Serializable]
    public class DamageType
    {
        public string name;
        public float armorSoak;
        public float armorReduction;
        public float psiSoak;
        public float psiReduction;
        public bool undodgeable;
    }
}
