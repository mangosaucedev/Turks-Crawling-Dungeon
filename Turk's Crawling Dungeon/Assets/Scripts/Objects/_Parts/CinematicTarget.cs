using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts
{
    public class CinematicTarget : Part
    {
        public static Dictionary<string, CinematicTarget> targets = new Dictionary<string, CinematicTarget>();

        public string cinematicId;

        public override string Name => "Cinematic Target";

        public string CinematicId
        { 
            get => cinematicId;
            set => cinematicId = value; 
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            if (!string.IsNullOrEmpty(cinematicId))
                targets[cinematicId] = this;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            if (targets.ContainsKey(cinematicId))
                targets.Remove(cinematicId);
        }
    }
}
