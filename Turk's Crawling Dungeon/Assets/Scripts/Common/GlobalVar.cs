using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    [Serializable]
    public class GlobalVar 
    {
        public string name;
        public object value;

        public GlobalVar(string name, object value)
        {
            this.name = name;
            this.value = value;
        }
    }
}
