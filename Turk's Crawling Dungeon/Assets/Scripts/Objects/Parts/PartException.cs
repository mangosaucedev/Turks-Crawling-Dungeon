using System;
using UnityEngine;

namespace TCD.Objects.Parts
{
    public abstract class PartException : Exception
    {
        public Part part;

        public PartException(Part part) : base()
        {
            this.part = part;
        }

        public PartException(Part part, string message) : base(message)
        {
            this.part = part;
        }
    }
}
