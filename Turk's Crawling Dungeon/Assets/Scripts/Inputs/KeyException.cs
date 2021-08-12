using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Inputs
{
    public class KeyException : Exception 
    {
        public KeyException() : base()
        {

        }

        public KeyException(string message) : base(message)
        {

        }
    }
}
