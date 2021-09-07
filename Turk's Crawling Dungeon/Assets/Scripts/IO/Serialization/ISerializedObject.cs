using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.IO.Serialization
{
    public interface ISerializedObject 
    {
        Guid Guid { get; set; }
    }
}
