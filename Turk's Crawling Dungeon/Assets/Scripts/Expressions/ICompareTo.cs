using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Expressions
{
    public interface ICompareTo : IReturnsBool
    {
        public Comparison Comparison { get; }

        public bool CompareNext { get; }
    }
}
