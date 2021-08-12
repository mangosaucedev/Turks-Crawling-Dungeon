using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class GameGridException : Exception
    {
        public GameGridException() : base()
        {

        }

        public GameGridException(string message) : base(message)
        {

        }
    }
}
