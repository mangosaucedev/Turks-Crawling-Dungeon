using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.UI
{
    public struct ActiveView 
    {
        public string name;
        public bool locksInput;
        public bool isInteractive;
        public GameObject gameObject;

        public ActiveView(string name, bool locksInput = true, bool isInteractive = true)
        {
            this.name = name;
            this.locksInput = locksInput;
            this.isInteractive = isInteractive;
            gameObject = null;
        }
    }
}
