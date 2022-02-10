using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;

namespace TCD
{
    public class DebugRawViewer : MonoBehaviour
    {
#if UNITY_EDITOR
        public List<ObjectBlueprint> objectBlueprints = new List<ObjectBlueprint>();
#endif
    }
}
