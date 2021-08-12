using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts
{
    public class Interaction
    {
        public string name;
        public string description;
        public delegate void OnInteract();
        public OnInteract onInteract;
        public bool isDefault;

        public Interaction(string name, OnInteract onInteract, bool isDefault = false)
        {
            this.name = name;
            this.onInteract = onInteract; 
            this.isDefault = isDefault;
        }

        public Interaction(string name, string description, OnInteract onInteract, bool isDefault = false) : 
            this(name, onInteract, isDefault)
        {
            this.description = description;
        }
    }
}
