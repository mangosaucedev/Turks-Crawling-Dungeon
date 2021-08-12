using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects
{
    public abstract class ObjectComponent
    {
        protected BaseObject parent;

        public ObjectComponent(BaseObject parent)
        {
            this.parent = parent;
        }
    }
}
