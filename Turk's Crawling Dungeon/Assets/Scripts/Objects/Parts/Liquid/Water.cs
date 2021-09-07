using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts
{
    [Serializable]
    public class Water : Liquid
    {
        public override string Name => "Water";

        protected override void OnDrink()
        {
            base.OnDrink();
        }
    }
}
