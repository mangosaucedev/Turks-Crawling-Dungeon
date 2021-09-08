using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;
using TCD.Objects.Parts;

namespace TCD.IO.Serialization
{
    [Serializable]
    public class SavedObject
    {
        public string blueprintName;
        public List<SavedPart> parts = new List<SavedPart>();
        public Vector2IntSurrogate position;
    
        [NonSerialized] private BaseObject baseObject;

        public SavedObject(BaseObject obj)
        {
            baseObject = obj;
            SerializeObject();
        }

        private void SerializeObject()
        {
            blueprintName = baseObject.name;
            position = new Vector2IntSurrogate(baseObject.cell.Position);
            foreach (Part part in baseObject.parts.Parts)
                parts.Add(new SavedPart(part));
        }

        public BaseObject DeserializeObject()
        {
            return null;
        }
    }
}
