using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    [AttributeUsage(AttributeTargets.Field, Inherited = false)]
    public class GameStaticAttribute : Attribute
    {
        public object value;

        public GameStaticAttribute(object value)
        {
            this.value = value;
        }
    }
}
