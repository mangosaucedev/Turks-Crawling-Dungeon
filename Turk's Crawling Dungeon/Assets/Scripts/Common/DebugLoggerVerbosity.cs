using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public enum DebugLoggerVerbosity
    {
        None,
        Time,
        Frame,
        TimeAndFrame = Time | Frame
    }
}
