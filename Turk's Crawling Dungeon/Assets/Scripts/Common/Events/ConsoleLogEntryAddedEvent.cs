using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class ConsoleLogEntryAddedEvent : Event
    {
        public string message;

        public override string Name => "Console Log Entry Added";

        public ConsoleLogEntryAddedEvent(string message) : base()
        {
            this.message = message;
        }
    }
}
