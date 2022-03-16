using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Expressions;

namespace TCD.Cinematics.Dialogues
{
    [Serializable]
    public class GoTo
    {
        public string node;
        public string when;

        public bool IsTraversable()
        {
            if (string.IsNullOrEmpty(when))
                return true;
            Expression expression = new Expression(when);
            return expression.isValid && expression.Result;
        }
    }
}
