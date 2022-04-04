using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class TransitionFromAny
    {
        public IState goTo;

        protected Func<bool> transition;

        public TransitionFromAny(Func<bool> transition, IState goTo)
        {
            this.transition = transition;
            this.goTo = goTo;
        }

        public bool CanTransition()
        {
            if (transition != null)
                return transition.Invoke();
            return false;
        }
    }
}
