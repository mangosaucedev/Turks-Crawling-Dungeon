using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects
{
    public static class PartUtility 
    {
        private static Dictionary<Guid, Part> parts = new Dictionary<Guid, Part>();

        public static bool TryGetFromGuid<T>(Guid id, out T part) where T : Part
        {
            part = null;
            if (!parts.TryGetValue(id, out Part p))
                return false;
            part = (T) p;
            return true;
        }

        public static void Add(Part part, Guid id) => parts[id] = part;

        public static void Remove(Guid id) => parts.Remove(id);
    }
}
