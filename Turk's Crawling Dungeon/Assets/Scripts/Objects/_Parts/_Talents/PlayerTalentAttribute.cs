using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts.Talents
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PlayerTalentAttribute : Attribute
    {
        public string name;

        public PlayerTalentAttribute(string name)
        {
            this.name = name;
        }
    }
}
