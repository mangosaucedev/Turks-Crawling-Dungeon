using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Encounters
{
    public static class EncounterFactory 
    {
        public static Encounter BuildFromBlueprint(string blueprintName)
        {
            Encounter blueprint = Assets.Get<Encounter>(blueprintName);
            Encounter encounter = (Encounter) blueprint.Clone();
            return encounter;
        }
    }
}
