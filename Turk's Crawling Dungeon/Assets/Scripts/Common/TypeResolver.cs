using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace TCD
{
    public static class TypeResolver 
    {
        public static Type ResolveType(string name)
        {
            Assembly assembly = typeof(TypeResolver).Assembly;
            return assembly.GetType(name, false, true);
        }
    }
}
