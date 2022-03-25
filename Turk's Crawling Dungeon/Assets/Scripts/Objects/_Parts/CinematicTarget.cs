using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts
{
    public class CinematicTarget : Part
    {
        public static Dictionary<string, CinematicTarget> targets = new Dictionary<string, CinematicTarget>();

        public string id;

        public override string Name => "Cinematic Target";

        protected override void OnEnable()
        {
            base.OnEnable();
            if (!string.IsNullOrEmpty(id))
                targets[id] = this;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            if (targets.ContainsKey(id))
                targets.Remove(id);
        }
    }
}
