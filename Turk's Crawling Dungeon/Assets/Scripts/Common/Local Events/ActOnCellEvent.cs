using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;

namespace TCD
{
    public abstract class ActOnCellEvent : LocalEvent
    {
        public BaseObject obj;
        public Cell cell;

        public BaseObject Object => obj;

        protected override void Reset()
        {
            obj = null;
            cell = null;
        }
    }
}
