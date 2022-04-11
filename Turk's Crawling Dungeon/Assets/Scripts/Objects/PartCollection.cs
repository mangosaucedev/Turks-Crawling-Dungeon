using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TCD.Objects
{
    [Serializable]
    public class PartCollection : ObjectComponent, IPartCollection
    {
        private List<Part> parts;

        public Transform PartsParent => parent.partsParent;

        public List<Part> Parts
        {
            get
            {
                if (parts == null)
                    parts = parent.GetComponentsInChildren<Part>().ToList();
                return parts;
            }
        }

        public PartCollection(BaseObject parent) : base(parent)
        {
            
        }

        public Part Add(Type type)
        {
            if (Has(type))
                return Get(type);
            Part part = (Part) PartsParent.gameObject.AddComponent(type);
            Parts.Add(part);
            return part;
        }

        public Part Get(Type type) 
        {
            foreach (Part part in Parts)
            {
                if (part.GetType() == type)
                    return part;
            }
            return null;
        }

        public T Get<T>() where T : Part
        {
            foreach (Part part in Parts)
            {
                if (part is T)
                    return (T) part;
            }
            return null;
        }

        public bool Has(Type type)
        {
            foreach (Part part in Parts)
            {
                Type partType = part.GetType();
                if (partType == type)
                    return true;
            }
            return false;
        }

        public bool Remove(Type type)
        {
            Part partToRemove = null;
            foreach (Part part in Parts)
            {
                Type partType = part.GetType();
                if (partType == type)
                {
                    partToRemove = part;
                    break;
                }
            }
            if (partToRemove != null)
            {
                Parts.Remove(partToRemove);
                Object.DestroyImmediate(partToRemove);
                return true;
            }
            return false;
        }

        public bool TryGet<T>(out T part) where T : Part
        {
            part = Get<T>();
            return part;
        }

        public bool TryGetList<T>(out List<T> partList) where T : Part
        {
            partList = new List<T>();
            foreach (Part part in Parts)
            {
                if (part is T)
                    partList.Add((T) part);
            }
            return partList.Count > 0;
        }

        public bool TryGet<T>(Type type, out T part) where T : Part
        {
            part = null;
            bool success = TryGet(type, out Part genericPart);
            if (success)
                part = (T) genericPart;
            return success;
        }

        public bool TryGet(Type type, out Part part)
        {
            part = Get(type);
            return part;
        }
    }
}
