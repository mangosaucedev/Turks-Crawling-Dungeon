using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts
{
    public class ItemGraphic : Graphic
    {
        public override string Name => "Item Graphic";

        protected override Sprite Sprite => Assets.Get<Sprite>("ItemGraphic");
    }
}
