using System;
using UnityEngine;

namespace TCD.Objects.Parts
{
    public class BrainException : PartException
    {
        public Brain brain;

        public BrainException(Brain brain) : base(brain)
        {
            this.brain = brain;
        }

        public BrainException(Brain brain, string message) :
            base(brain, message)
        {
            this.brain = brain;
        }
    }
}
