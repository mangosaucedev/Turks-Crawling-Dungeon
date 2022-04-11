using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCD.Objects;

namespace TCD.TimeManagement
{
    public struct ScheduledAction
    {
        public BaseObject actor;
        public Action action;

        public ScheduledAction(BaseObject actor, Action action)
        {
            this.actor = actor;
            this.action = action;
        }
    }
}
