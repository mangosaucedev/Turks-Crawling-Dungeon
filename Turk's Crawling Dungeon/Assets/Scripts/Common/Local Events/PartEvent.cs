using System.Collections;
using System.Collections.Generic;
using TCD.Objects;

namespace TCD
{
    public abstract class PartEvent : ObjectEvent
    {
        public abstract Part Part { get; }
    }
}
