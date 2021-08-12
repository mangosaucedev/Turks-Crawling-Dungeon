using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Attacks
{
    public class Attack : ICloneable
    {
        public string name;
        public string verb;
        public string verbPastTense;
        public int minDamage;
        public int maxDamage;
        public string attackTypeName;
        public AttackType attackType;
        public string damageTypeName;
        public DamageType damageType;
        public int hitAccuracy;
        public float weight;
        public BaseObject weapon;

        public object Clone() => MemberwiseClone();

        public float GetAverageDamage() => (minDamage + maxDamage) / 2;
    }
}
