using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Expressions;

namespace TCD.Cinematics.Dialogues
{
    [Serializable]
    public class Choice : Element
    {
        public string prerequisite;

        public bool IsEnabled()
        {
            if (string.IsNullOrEmpty(prerequisite))
                return true;
            Expression expression = new Expression(prerequisite);
            return expression.isValid && expression.Result;
        }
    }
}
