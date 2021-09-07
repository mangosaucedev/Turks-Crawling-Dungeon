using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts
{
    public class Blood : Liquid
    {
        public override string Name => "Blood";

        protected override void OnDrink()
        {
            base.OnDrink();
        }
    }
}
