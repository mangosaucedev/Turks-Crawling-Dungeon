using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class ParentManager : MonoBehaviour
    {
        private const string OBJECTS = "--- Objects ---";
        private const string CANVAS = "Main Canvas";
        private const string OVER_SCREEN = "Over Screen Canvas";
        private const string TEMP = "--- Temp ---";
        private const string PREFABS = "--- Prefabs ---";
        private const string TALENT_INSTANCES = "Talent Instances";

        private static Transform objects;
        private static Transform canvas;
        private static Transform overScreen;
        private static Transform temp;
        private static Transform prefabs;
        private static Transform talentInstances;

        public static Transform Objects => Find(ref objects, OBJECTS);

        public static Transform Canvas => Find(ref canvas, CANVAS);

        public static Transform OverScreen => Find(ref overScreen, OVER_SCREEN);

        public static Transform Temp => Find(ref temp, TEMP);

        public static Transform Prefabs => Find(ref prefabs, PREFABS);

        public static Transform TalentInstances => Find(ref talentInstances, TALENT_INSTANCES);

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
            ExceptionHandler.Handle(new ParentManagerException("Could not find parent transform named " + name));
            return null;
        }
    }
}
