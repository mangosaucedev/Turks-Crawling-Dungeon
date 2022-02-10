using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public enum DebugLoggerPrefix
    {
        None,
        Time,
        Frame,
        TimeAndFrame = Time | Frame
    }
}
