using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts
{
    public class Outlined : Part
    {
        public override string Name => "Outlined";

        protected override void Start()
        {
            base.Start();
            parent.SpriteRenderer.material = Assets.Get<Material>("Outlined");
        }
    }
}
