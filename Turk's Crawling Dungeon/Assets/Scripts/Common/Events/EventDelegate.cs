using System;
using System.Collections.Generic;
using System.Text;

namespace TCD
{
    public delegate void EventDelegate<in T>(T e) where T : Event;
}
