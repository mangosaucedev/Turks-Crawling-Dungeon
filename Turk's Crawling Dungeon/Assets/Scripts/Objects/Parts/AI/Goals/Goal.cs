using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts
{
    public abstract class Goal
    {
        public Goal parent;
        protected Brain brain;
        protected BaseObject obj;

        public string Name => GetType().Name;

        protected Vector2Int Position => obj.cell.Position;

        protected Cell CurrentCell => obj.cell.CurrentCell;

        protected AIQuery Query => brain.query;

        public Goal(Brain brain)
        {
            this.brain = brain;
            obj = this.brain.parent;
            OnInstantiation();
        }

        public abstract int GetTimeCost();

        public virtual void PerformAction()
        {

        }

        public virtual bool IsFinished() => false;

        protected virtual void PushGoal() => brain.goals.Push(this);
        
        protected virtual void PushGoal(Goal goal) => brain.goals.Push(goal);

        protected virtual void PushChildGoal(Goal goal)
        {
            goal.parent = this;
            goal.PushGoal();
        }

        protected virtual void Pop() => brain.goals.Pop();
        
        protected virtual void FailToParent()
        {
            while (brain.goals.Count > 0 && brain.goals.Peek() != parent)
            {
                Goal goal = brain.goals.Peek();
                goal.OnFail();
                Pop();
            }
        }

        protected virtual void OnFail()
        {

        }

        protected virtual void OnInstantiation()
        {

        }

        protected virtual void Think(string thought) => 
            brain.Think(thought);
        
    }
}