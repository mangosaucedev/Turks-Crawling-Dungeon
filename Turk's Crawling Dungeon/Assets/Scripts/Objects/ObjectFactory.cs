using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TCD.Objects
{
    public static class ObjectFactory
    {
        private static GameObject currentGameObject;
        private static BaseObject currentObject;

        public static BaseObject BuildFromPrefab(GameObject prefab, Vector2Int position)
        {
            if (!prefab)
                prefab = Assets.Get<GameObject>("Base Object");
            Transform parent = ParentManager.Objects;
            currentGameObject = Object.Instantiate(prefab, parent);
            currentGameObject.SetActive(true);
            currentObject = currentGameObject.GetComponent<BaseObject>();
            currentGameObject.name = prefab.name.Replace("(Clone)","");
            currentObject.cell.SetPosition(position);
            return currentObject;
        }

        public static BaseObject BuildFromBlueprint(string blueprint, Vector2Int position)
        {
            if (Assets.Exists<GameObject>(blueprint))
            {
                GameObject prefab = Assets.Get<GameObject>(blueprint);
                return BuildFromPrefab(prefab, position);
            }
            ObjectBlueprint blueprintInstance;
            try
            {
                blueprintInstance = Assets.Get<ObjectBlueprint>(blueprint);
            }
            catch
            {
                throw new ObjectFactoryException($"Invalid blueprint '{blueprint}'!");
            }
            return BuildFromBlueprint(blueprintInstance, position);
        }

        public static BaseObject BuildFromBlueprint(ObjectBlueprint blueprint, Vector2Int position)
        {
            GameObject prefab = BuildPrefabFromBlueprint(blueprint);
            BuildFromPrefab(prefab, position);
            return currentObject;
        }

        public static GameObject BuildPrefabFromBlueprint(ObjectBlueprint blueprint)
        {
            GameObject prefab = null;
            if (Assets.Exists<GameObject>(blueprint.name))
            {             
                prefab = Assets.Get<GameObject>(blueprint.name);
                return Object.Instantiate(prefab);
            }
            if (!string.IsNullOrEmpty(blueprint.inheritsFrom))
                prefab = GetInheritedPrefab(blueprint.inheritsFrom);
            else
                prefab = Assets.Get<GameObject>("Base Object");
            prefab.SetActive(true);
            GameObject gameObject = Object.Instantiate(prefab);
            prefab.SetActive(false);
            gameObject.SetActive(false);
            gameObject.name = blueprint.name;

            BaseObject obj = gameObject.GetComponent<BaseObject>();

            AttachPartsFromBlueprint(obj, blueprint);
            OverridePartsFromBlueprint(obj, blueprint);
            RemovePartsFromBlueprint(obj, blueprint);

            Assets.Add(blueprint.name, gameObject);
            return gameObject;
        }

        private static GameObject GetInheritedPrefab(string inheritsFrom)
        {
            if (Assets.Exists<GameObject>(inheritsFrom))
                return Assets.Get<GameObject>(inheritsFrom);
            ObjectBlueprint blueprint = Assets.Get<ObjectBlueprint>(inheritsFrom);
            return BuildPrefabFromBlueprint(blueprint);
        }

        private static void AttachPartsFromBlueprint(BaseObject obj, ObjectBlueprint blueprint)
        {
            foreach (string partName in blueprint.partNames)
            {
                PartBlueprint partBlueprint = blueprint.partsByName[partName];
                Type type = partBlueprint.GetPartType();
                if (!obj.parts.Has(type))
                {
                    Part part = obj.parts.Add(type);
                    SetPartFields(part, partBlueprint);
                }
            }
        }

        private static void SetPartFields(Part part, PartBlueprint blueprint)
        {     
            foreach (string var in blueprint.variables)
            {
                object value = blueprint.variablesByName[var];
                SetPartProperty(part, var, value);
            }
        }

        private static void SetPartProperty(Part part, string property, object value)
        {
            Type type = part.GetType();
            object obj = part;
            PropertyInfo propertyInfo = type.GetProperty(property);
            try
            {
                if (propertyInfo.PropertyType == typeof(bool))
                    propertyInfo.SetValue(obj, Convert.ToBoolean(value), null);
                if (propertyInfo.PropertyType == typeof(string))
                    propertyInfo.SetValue(obj, Convert.ToString(value), null);
                if (propertyInfo.PropertyType == typeof(double))
                    propertyInfo.SetValue(obj, Convert.ToDouble(value), null);
                if (propertyInfo.PropertyType == typeof(int))
                    propertyInfo.SetValue(obj, Convert.ToInt32(value), null);
                if (propertyInfo.PropertyType == typeof(short))
                    propertyInfo.SetValue(obj, Convert.ToInt16(value), null);
                if (propertyInfo.PropertyType == typeof(float))
                    propertyInfo.SetValue(obj, Convert.ToSingle(value), null);
            }
            catch (Exception e)
            {
                throw new ObjectFactoryException(
                    $"{part.Name} failed to write field {property}! - " + e.Message);
            }
        }

        private static void OverridePartsFromBlueprint(BaseObject obj, ObjectBlueprint blueprint)
        {
            foreach (string partName in blueprint.overridenPartNames)
            {
                PartBlueprint partBlueprint = blueprint.overridenPartsByName[partName];
                Type type = partBlueprint.GetPartType();
                if (obj.parts.Has(type))
                {
                    Part part = obj.parts.Get(type);
                    SetPartFields(part, partBlueprint);
                }
            }
        }

        private static void RemovePartsFromBlueprint(BaseObject obj, ObjectBlueprint blueprint)
        {
            foreach (string partName in blueprint.removedParts)
            {
                Type type = TypeResolver.ResolveType($"TCD.Objects.Parts.{partName}");
                obj.parts.Remove(type);
            }
        }
    }
}
