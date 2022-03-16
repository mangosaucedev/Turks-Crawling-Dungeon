using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Expressions
{
    public class ExpressionParseException : Exception
    {

        public ExpressionParseException() : base()
        {

        }

        public ExpressionParseException(string message) : base(message)
        {

        }
    }
}
