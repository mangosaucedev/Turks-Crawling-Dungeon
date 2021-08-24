using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public abstract class AICommandEvent : ActOnObjectEvent
    {
        public bool hasActed;

        protected override void Reset()
        {
            base.Reset();
            hasActed = false;
        }
    }
}
