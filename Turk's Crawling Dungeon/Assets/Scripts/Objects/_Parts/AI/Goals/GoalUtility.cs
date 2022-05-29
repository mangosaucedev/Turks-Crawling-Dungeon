using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts
{
    public static class GoalUtility
    {
        private static Dictionary<Guid, Goal> goals = new Dictionary<Guid, Goal>();

        public static bool TryGetFromGuid<T>(Guid id, out T goal) where T : Goal
        {
            goal = null;
            if (!goals.TryGetValue(id, out Goal g))
                return false;
            goal = (T) g;
            return true;
        }

        public static void Add(Goal goal, Guid id) => goals[id] = goal;

        public static void Remove(Guid id) => goals.Remove(id);
    }
}
