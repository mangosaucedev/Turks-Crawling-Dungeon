using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Attacks;

namespace TCD.Objects.Parts
{
    public class StandardPierceInjury : StandardInjury
    {
        public StandardPierceInjury(Attack attack) : base(attack)
        {

        }

        protected override void LoadAttack(Attack attack)
        {

        }

        public override void OnAcquire()
        {

        }

        public override void OnHealed()
        {

        }

        public override float GetSeverity()
        {
            return 0f;
        }

        public override string GetName()
        {
            return "";
        }

        public override string GetDescription()
        {
            return "";
        }
    }
}
