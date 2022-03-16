using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects
{
    public class LocalVar
    {
        public string name;
        public object value;

        public LocalVar(string name, object value)
        {
            this.name = name;
            this.value = value;
        }
    }
}
