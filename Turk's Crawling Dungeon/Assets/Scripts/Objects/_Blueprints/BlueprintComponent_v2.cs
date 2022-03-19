using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Blueprints
{
    [Serializable]
    public class BlueprintComponent_v2 
    {
        private const string PATTERN = @"(\((?<namespace>[a-z]+)\))?(?<type>[a-z0-9_]+)";

        public string type;
        public BlueprintField_v2[] fields;
    }
}
