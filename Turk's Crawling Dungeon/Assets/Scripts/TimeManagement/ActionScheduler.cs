using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using TCD.Objects;
using TCD.Objects.Parts;

namespace TCD.TimeManagement
{
    public class ActionScheduler : MonoBehaviour
    {
        private static BlockingCollection<ScheduledAction> inputActions = new BlockingCollection<ScheduledAction>();
        private static Queue<ScheduledAction> outputActions = new Queue<ScheduledAction>();


        public static void EnqueueAction(BaseObject actor, Action action, ActionType type = ActionType.Object) =>
            EnqueueAction(actor.id, action, type);

        public static void EnqueueAction(Guid id, Action action, ActionType type = ActionType.Object)
        { 
            ScheduledAction scheduledAction = new ScheduledAction(id, action, type);
            inputActions.Add(scheduledAction);
        }

        public static void PerformQueue()
        {
            inputActions.CompleteAdding();
            foreach (var action in inputActions)
                outputActions.Enqueue(action);
            inputActions.Dispose();
            inputActions = new BlockingCollection<ScheduledAction>();
            DebugLogger.Log($"[Action Scheduler] - Executing {outputActions.Count} actions.");   

            while (outputActions.Count > 0)
            {
                ScheduledAction scheduledAction = outputActions.Dequeue();
                BaseObject actor = null;
                Part part = null;
                Goal goal = null;
                if ((scheduledAction.type == ActionType.Object && !ObjectUtility.TryGetFromId(scheduledAction.id, out actor))
                    || (scheduledAction.type == ActionType.Part && !PartUtility.TryGetFromGuid(scheduledAction.id, out part))
                    || (scheduledAction.type == ActionType.Goal && !GoalUtility.TryGetFromGuid(scheduledAction.id, out goal)))
                    continue;
                if (!actor && !part && goal == null)
                    continue;
                scheduledAction.action?.Invoke();
            }
            outputActions.Clear();
        } 
    }
}
