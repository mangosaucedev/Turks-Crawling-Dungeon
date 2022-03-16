using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts
{
    public class FactionAllegiance : Part
    {
        public string factionName;

        public override string Name => "Faction Allegiance";
    
        public string FactionName
        {
            get => factionName;
            set => factionName = value;
        }

        public Faction Faction => Assets.Get<Faction>(FactionName);
    }
}
