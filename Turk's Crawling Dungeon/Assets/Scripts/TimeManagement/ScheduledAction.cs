using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCD.Objects;

namespace TCD.TimeManagement
{
    public struct ScheduledAction
    {
        public Guid id;
        public Action action;
        public ActionType type;

        public ScheduledAction(Guid id, Action action, ActionType type)
        {
            this.id = id;
            this.action = action;
            this.type = type;
        }
    }
}
