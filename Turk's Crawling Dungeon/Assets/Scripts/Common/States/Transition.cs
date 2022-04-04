using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class Transition : TransitionFromAny
    {
        public readonly IState state;

        public Transition(IState state, Func<bool> transition, IState goTo) : base(transition, goTo)
        {
            this.state = state;
        }
    }
}
