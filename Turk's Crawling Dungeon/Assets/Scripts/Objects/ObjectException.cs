using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects
{
    public class ObjectException : Exception
    {
        public ObjectException() : base()
        {

        }

        public ObjectException(string message) : base(message)
        {

        }
    }
}
