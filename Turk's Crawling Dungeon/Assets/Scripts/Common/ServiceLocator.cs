using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class ServiceLocator : MonoBehaviour
    {
        private static Dictionary<Type, MonoBehaviour> services = new Dictionary<Type, MonoBehaviour>();

        public static T Get<T>() where T : MonoBehaviour
        {
            Type type = typeof(T);
            if (!services.TryGetValue(type, out MonoBehaviour service) || !service)
            {
                service = FindObjectOfType<T>();
                if (service)
                    services[type] = service;
                else
                    ExceptionHandler.Handle(new NullReferenceException(
                        string.Format("Service locator not locate service {0}", type.Name)));
            }
            return (T) service;
        }
    }
}