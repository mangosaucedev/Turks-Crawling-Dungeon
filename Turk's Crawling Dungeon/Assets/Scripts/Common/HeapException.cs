using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class HeapException : Exception 
    {
        public HeapException() : base()
        {

        }

        public HeapException(string message) : base(message)
        {

        }
    }
}
