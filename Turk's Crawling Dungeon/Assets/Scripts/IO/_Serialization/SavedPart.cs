using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using TCD.Objects;
using TCD.Objects.Parts;

namespace TCD.IO.Serialization
{
    [Serializable]
    public class SavedPart
    {
        public string name;
        public FieldInfo[] fields;

        [NonSerialized] private Part basePart;

        public SavedPart(Part part)
        {
            basePart = part;
            SerializePart();
        }

        private void SerializePart()
        {
            name = basePart.GetType().FullName;
            fields = basePart.GetType().GetFields();
        }

        public Part DeserializePart()
        {
            return null;
        }
    }
}
