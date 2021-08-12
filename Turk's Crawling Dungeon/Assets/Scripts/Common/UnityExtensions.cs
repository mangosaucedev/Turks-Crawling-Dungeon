using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public static class UnityExtensions
    {
        public static void EnsureCoroutineStopped(this MonoBehaviour monoBehaviour, ref Coroutine coroutine)
        {
            if (coroutine != null)
                monoBehaviour.StopCoroutine(coroutine);
            coroutine = null;
        }

        public static Color With(
            this Color c, float? r = null, float? g = null, float? b = null, float? a = null)
        {
            Color newColor = c;
            newColor.r = r ?? c.r;
            newColor.g = g ?? c.g;
            newColor.b = b ?? c.b;
            newColor.a = a ?? c.a;
            return newColor;
        }

        public static BoundsInt ToBoundsInt(this Bounds bounds)
        {
            Vector3Int position = new Vector3Int((int)bounds.min.x, (int)bounds.min.y, 0);
            Vector3Int size = new Vector3Int((int)bounds.size.x, (int)bounds.size.y, 0);
            return new BoundsInt(position, size);
        }

        public static Bounds ToBounds(this BoundsInt boundsInt)
        {
            return new Bounds(boundsInt.center, boundsInt.size);
        }

        public static T GetComponentInChildren<T>(this Component _this, Predicate<T> predicate) where T : Component
        {
            var components = _this.GetComponentsInChildren<T>();
            foreach (T component in components)
            {
                if (predicate(component))
                    return component;
            }
            return null;
        }

        public static bool TryGetComponentInParent<T>(this Component _this, out T component) where T : Component
        {
            component = _this.GetComponentInParent<T>();
            return component;
        }

        public static bool TryGetComponentInParent<T>(this GameObject _this, out T component) where T : Component
        {
            component = _this.GetComponentInParent<T>();
            return component;
        }

        public static Vector2 With(this Vector2 vector, float? x, float? y)
        {
            return new Vector2(x ?? vector.x, y ?? vector.y);
        }

        public static Vector3 With(this Vector3 vector, float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(x ?? vector.x, y ?? vector.y, z ?? vector.z);
        }

        public static Vector3Int ToInt(this Vector3 vector)
        {
            return new Vector3Int((int)vector.x, (int)vector.y, (int)vector.z);
        }
    }
}
