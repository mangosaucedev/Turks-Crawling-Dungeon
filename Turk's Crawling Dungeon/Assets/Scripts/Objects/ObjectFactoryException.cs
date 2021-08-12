using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class ObjectFactoryException : Exception
    {
        public ObjectFactoryException() : base()
        {

        }

        public ObjectFactoryException(string message) : base(message)
        {

        }
    }
}
