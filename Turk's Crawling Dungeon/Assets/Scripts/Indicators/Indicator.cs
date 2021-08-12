using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Indicators
{
    public abstract class Indicator : MonoBehaviour
    { 
        public Vector2Int startPosition;
        public GameObject targetObject;
        public Vector2Int targetPosition;

        public virtual void Start()
        {

        }
    }
}
