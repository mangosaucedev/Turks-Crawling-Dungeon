using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.IO
{
    public class RawException : Exception
    {
        public RawException() : base()
        {

        }

        public RawException(string message) : base(message)
        {

        }
    }
}
