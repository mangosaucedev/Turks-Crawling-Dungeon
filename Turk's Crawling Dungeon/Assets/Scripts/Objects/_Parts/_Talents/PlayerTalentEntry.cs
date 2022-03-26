using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts.Talents
{
    public struct PlayerTalentEntry 
    {
        public Type type;
        public string name;

        public PlayerTalentEntry(Type type, string name)
        {
            this.type = type;
            this.name = name;
        }
    }
}
