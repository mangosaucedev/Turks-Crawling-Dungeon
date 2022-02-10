using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts
{
    public class GoalCollection
    {
        private Brain brain;
        private List<Goal> collection = new List<Goal>();

        public Goal this[int index]
        {
            get
            {
                if (Count >= index)
                    return collection[index];
                ExceptionHandler.Handle(new BrainException(
                    brain,
                    $"Index out of range of goal collection: {index} " +
                    $"({Count} items in collection!)"));
                return null;
            }
        }

        public int Count => collection.Count;

        public void Push(Goal goal)
        {
            brain.Think("pushing goal " + goal.Name);
            collection.Add(goal);
        }

        public GoalCollection(Brain brain)
        {
            this.brain = brain;
        }

        public Goal Pop()
        {
            if (Count > 0)
            {
                Goal goal = collection[Count - 1];
                collection.Remove(goal);
                brain.Think("popping goal " + goal.Name);
                return goal;
            } 
            return null;
        }

        public Goal Peek()
        {
            if (Count > 0)
                return collection[Count - 1];
            return null;
        }
    }
}