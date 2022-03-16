using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Cinematics
{
    public class CinematicTarget : MonoBehaviour
    {
        public static Dictionary<string, CinematicTarget> targets = new Dictionary<string, CinematicTarget>();

        public string id;

        private void OnEnable()
        {
            if (!string.IsNullOrEmpty(id))
                targets[id] = this;
        }

        private void OnDisable()
        {
            if (targets.ContainsKey(id))
                targets.Remove(id);
        }
    }
}
