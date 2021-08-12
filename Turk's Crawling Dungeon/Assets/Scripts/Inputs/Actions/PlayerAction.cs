using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;   

namespace TCD.Inputs.Actions
{
    public abstract class PlayerAction 
    {
        public abstract string Name { get; }

        public abstract MovementMode MovementMode { get; }

        public virtual bool IsUsable()
        {
            return true;
        }

        public abstract int GetRange();

        public virtual void Start()
        {

        }

        public virtual void End()
        {

        }

        public abstract IEnumerator OnObject(BaseObject target);

        public abstract IEnumerator OnCell(Cell cell);
    }
}
