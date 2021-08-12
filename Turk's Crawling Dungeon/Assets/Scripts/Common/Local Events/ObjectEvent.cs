using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;

namespace TCD
{
    public abstract class ObjectEvent : LocalEvent
    {
        public abstract BaseObject Object { get; }
    }
}
