using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts
{
    [Obsolete]
    public abstract class Tiling : Part
    {
        public bool isDisabled;

        public abstract string Single { get; set; }

        protected abstract void UpdateSurroundingSprites();
    }
}
