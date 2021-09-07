using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts
{
    [Serializable]
    public class Acid : Liquid
    {
        public override string Name => "Acid";

        protected override void OnDrink()
        {
            base.OnDrink();
        }
    }
}
