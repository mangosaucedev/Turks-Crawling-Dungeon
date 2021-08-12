using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class ParentManagerException : Exception
    {
        public ParentManagerException() : base()
        {

        }

        public ParentManagerException(string message) : base(message)
        {

        }
    }
}
