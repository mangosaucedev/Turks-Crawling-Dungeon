using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.IO.Serialization
{
    [Serializable]
    public class SavedGame 
    {
        public List<SavedObject> savedObjects = new List<SavedObject>();
    }
}
