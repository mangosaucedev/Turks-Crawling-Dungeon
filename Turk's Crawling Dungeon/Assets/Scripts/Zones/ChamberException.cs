using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Zones
{
    public class ChamberException : Exception
    {
        public ChamberException() : base()
        {

        }

        public ChamberException(string message) : base(message)
        {

        }
    }
}
