using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;

namespace TCD.TimeManagement
{
    public static class ActionScheduler 
    {
        private static Queue<ScheduledAction> actions = new Queue<ScheduledAction>();

        public static void EnqueueAction(BaseObject actor, Action action)
        {
            ScheduledAction scheduledAction = new ScheduledAction(actor, action);
            actions.Enqueue(scheduledAction);
        }

        public static void PerformQueue()
        {
            while (actions.Count > 0)
            {
                ScheduledAction scheduledAction = actions.Dequeue();
                if (!scheduledAction.actor)
                    continue;
                scheduledAction.action?.Invoke();
            }
            actions.Clear();
        }
    }
}
