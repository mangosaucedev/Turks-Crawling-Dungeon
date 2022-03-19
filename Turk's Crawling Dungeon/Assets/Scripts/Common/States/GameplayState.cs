using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public abstract class GameplayState : IState
    {
        public abstract string Name { get; }

        public virtual void Start()
        {

        }

        public virtual void Update()
        {

        }

        public virtual void FixedUpdate()
        {

        }

        public virtual void SlowUpdate()
        {

        }

        public virtual void End()
        {

        }
    }
}
