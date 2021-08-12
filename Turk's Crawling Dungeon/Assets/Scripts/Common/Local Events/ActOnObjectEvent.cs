using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;

namespace TCD
{
    public abstract class ActOnObjectEvent : LocalEvent
    {
        public BaseObject obj;

        protected override void Reset()
        {
            obj = null;
        }
    }
}
