using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Juice
{
    public abstract class JuiceAnimation 
    {
        public bool hasStarted;
        public BaseObject obj;

        public JuiceAnimation(BaseObject obj)
        {
            this.obj = obj;
        }

        public virtual bool CanPerform()
        {
            return true;
        }

        public virtual bool IsFinished()
        {
            return true;
        }

        public virtual void Start()
        {

        }

        public virtual void Update()
        {

        }
        
        public virtual void End()
        {

        }
    }
}
