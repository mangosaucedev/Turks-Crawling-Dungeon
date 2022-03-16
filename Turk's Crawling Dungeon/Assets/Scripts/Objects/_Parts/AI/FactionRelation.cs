using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts
{
    [Serializable]
    public class FactionRelation 
    {
        public string faction;
        public int relation;

        public Faction Faction => Assets.Get<Faction>(faction);
    }
}
