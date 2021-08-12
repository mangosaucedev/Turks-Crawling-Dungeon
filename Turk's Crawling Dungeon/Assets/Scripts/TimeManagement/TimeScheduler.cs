using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wintellect.PowerCollections;
using TCD.Objects;
using TCD.Objects.Parts;

namespace TCD.TimeManagement
{
    public class TimeScheduler : MonoBehaviour
    {
        public static int timeElapsed;
        public static int timeElapsedLastTurn;
        private static List<Actor> actors = new List<Actor>();
        private static Actor currentActor;

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
            MessageLog.Add("--- end turn ---");
            EventManager.Send(new BeforeTurnTickEvent(timeElapsedThisTurn));
            PassTime(timeElapsedThisTurn);
            if (actors.Count > 0)
                UpdateActors();
            EventManager.Send(new AfterTurnTickEvent(timeElapsedThisTurn));
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
