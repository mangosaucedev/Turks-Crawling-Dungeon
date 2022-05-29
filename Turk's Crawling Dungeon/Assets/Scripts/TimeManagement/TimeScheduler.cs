using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Inputs;
using TCD.Objects;
using TCD.Objects.Parts;
using TCD.Pathfinding;
using TCD.Threading;

namespace TCD.TimeManagement
{
    public class TimeScheduler : MonoBehaviour
    {
        public static int timeElapsed;
        public static int timeElapsedLastTurn;
        private static List<Actor> actors = new List<Actor>();
        private static Actor currentActor;

        public readonly object _lock = new object();

        public static bool AddActor(Actor actor)
        {
            if (!actors.Contains(actor))
            {
                actors.Add(actor);
                return true;
            }
            return false;
        }

        public static bool RemoveActor(Actor actor)
        {
            if (actors.Contains(actor))
            {
                actors.Remove(actor);
                return true;
            }
            return false;
        }

        public static void Tick(int timeElapsedThisTurn)
        {
            lock (ServiceLocator.Get<TimeScheduler>()._lock)
            {
                MessageLog.Add("--- end turn ---");
                EventManager.Send(new BeforeTurnTickEvent(timeElapsedThisTurn));
                PassTime(timeElapsedThisTurn);

                if (actors.Count > 0)
                    UpdateActors();

                //DebugLogger.Log("[Time] - Waiting for threads to join.");
                //JobManager.WaitFor(JobGroup.Pathfinding);
                //JobManager.WaitFor(JobGroup.Fluid);

                //DebugLogger.Log("[Time] - Threads joined. Pathfinding manager outputting paths...");
                //ServiceLocator.Get<PathfindingManager>().OutputPaths();

                DebugLogger.Log("[Time] - Performing action queue.");
                ActionScheduler.PerformQueue();
                DebugLogger.Log("[Time] - Action queue performed. Culling objects.");
                ObjectCuller.PerformCulling();
                EventManager.Send(new AfterTurnTickEvent(timeElapsedThisTurn));
                DebugLogger.Log("[Time] - Turn processed!");
            }
        }

        private static void UpdateActors()
        {
            for (int i = actors.Count - 1; i >= 0; i--)
            {
                if (i > actors.Count - 1)
                    continue;
                Actor actor = actors[i];
                actor.Act(timeElapsedLastTurn);
            }
        }

        private static void PassTime(int time)
        {
            timeElapsed += time;
            timeElapsedLastTurn = time;
        }
    }
}
