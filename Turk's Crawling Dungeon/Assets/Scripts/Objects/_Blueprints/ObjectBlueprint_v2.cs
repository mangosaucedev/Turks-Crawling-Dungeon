using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Blueprints
{
    [Serializable]
    public class ObjectBlueprint_v2
    {
        public string name;
        public string inherits;
        public BlueprintComponent_v2[] components;
        public string[] removedComponents;
    }
}
