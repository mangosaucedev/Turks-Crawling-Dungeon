using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ConsoleCommandAttribute : Attribute
    {
        public string command;

        public ConsoleCommandAttribute(string command)
        { 
            this.command = command;
        }
    }
}