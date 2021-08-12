using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class DebugBlueprintPrefabs : MonoBehaviour
    {
#if UNITY_EDITOR
        public List<GameObject> prefabs = new List<GameObject>();
#endif
    }
}
