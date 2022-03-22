using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Zones.Environments
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ZoneFurnisherAttribute : Attribute
    {
        public string name;

        public ZoneFurnisherAttribute(string name)
        {
            this.name = name;
        }
    }
}
