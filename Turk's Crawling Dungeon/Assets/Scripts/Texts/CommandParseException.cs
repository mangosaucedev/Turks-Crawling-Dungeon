using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Texts
{
    public class CommandParseException : Exception
    {
        public const string TYPE_NOT_FOUND = "Failed to parse command type!";
        public const string INVALID_TYPE = "Could not instantiate command object: invalid type!";

        public CommandParseException() : base()
        {

        }

        public CommandParseException(string message) : base(message)
        {

        }
    }
}
