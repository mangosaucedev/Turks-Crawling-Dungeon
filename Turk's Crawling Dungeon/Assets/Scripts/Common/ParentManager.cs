using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class ParentManager : MonoBehaviour
    {
        private const string OBJECTS = "--- Objects ---";
        private const string CANVAS = "Main Canvas";
        private const string TEMP = "--- Temp ---";

        private static Transform objects;
        private static Transform canvas;
        private static Transform temp;

        public static Transform Objects => Find(ref objects, OBJECTS);

        public static Transform Canvas => Find(ref canvas, CANVAS);

        public static Transform Temp => Find(ref temp, TEMP);

        private void OnEnable()
        {

        }

        private void OnDisable()
        {
            
        }

        private static Transform Find(ref Transform transform, string name)
        {
            if (!transform)
                transform = GameObject.Find(name)?.transform;
            if (transform)
                return transform;
            throw new ParentManagerException("Could not find parent transform named " + name);
        }
    }
}
