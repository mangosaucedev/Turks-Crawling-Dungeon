using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Juice
{
    public class JuiceAnimation 
    {
        public bool hasStarted;

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
