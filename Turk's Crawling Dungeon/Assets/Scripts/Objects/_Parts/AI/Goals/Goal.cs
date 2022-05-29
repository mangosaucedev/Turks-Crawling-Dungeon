using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts
{
    public abstract class Goal
    {
        public Goal parent;
        public Guid id = Guid.NewGuid();

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

        public virtual bool PerformAction()
        {
            return true;
        }

        public virtual bool IsFinished() => false;

        protected virtual void PushGoal() => brain.Push(this);
        
        protected virtual void PushGoal(Goal goal) => brain.Push(goal);

        protected virtual void PushChildGoal(Goal goal)
        {
            goal.parent = this;
            goal.PushGoal();
        }

        protected virtual void Pop() => brain.Pop();
        
        protected virtual void FailToParent()
        {
            while (brain.Count > 0 && brain.Peek() != parent)
            {
                Goal goal = brain.Peek();
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

        public virtual void Think(string thought) => 
            brain.Think(thought);
        
    }
}