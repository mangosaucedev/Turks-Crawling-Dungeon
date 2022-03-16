using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Texts
{
    public class DisplayVariable : Command
    {
        public override CommandType Type => CommandType.DisplayVariable;

        protected override void ApplyCommandToTargetText(ref string text)
        {
            string variableName = arguments[0];
            object value = GlobalVars.Get(variableName);
            string variable = value == null ? "{UNSET VARIABLE}" : value.ToString(); 
            text = variable;
        }
    }
}
