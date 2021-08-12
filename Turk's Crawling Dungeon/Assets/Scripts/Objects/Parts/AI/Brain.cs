using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts
{
    public class Brain : Part
    {
        private const int MAX_THOUGHTS = 32;

        [Header("AI Parameters")]
        [SerializeField] private int sightRadius;

        public AIQuery query;
        public GoalCollection goals;
#if UNITY_EDITOR
        [Header("Debug Info")]
        public List<Vector2Int> path = new List<Vector2Int>();
        [SerializeField] private string currentGoal;
        [SerializeField, TextArea(3,5)] private string currentGoalStatus;
#endif
        [Header("Thoughts")]
        [SerializeField] private List<string> thoughts = new List<string>();

        private int energy;

        public int SightRadius
        {
            get => sightRadius;
            set => sightRadius = value;
        }

        public override string Name => "Brain";

        public Goal CurrentGoal => goals.Peek();

        protected override void Awake()
        {
            base.Awake();
            goals = new GoalCollection(this);
            query = new AIQuery(this);
        }

        public override bool HandleEvent<T>(T e)
        {
            if (e.Id == ActorTakeTurnEvent.id)
                OnTakeTurn(e);
            return base.HandleEvent(e);
        }

        private void OnTakeTurn(LocalEvent e)
        {
            ActorTakeTurnEvent takeTurnEvent = (ActorTakeTurnEvent) e;
            energy = takeTurnEvent.energy;

            PopFinishedGoals();
            if (goals.Count == 0)
                goals.Push(new Idle(this));
            if (goals.Count > 0)
                PerformSequentialActions();
            takeTurnEvent.energy = energy;

#if UNITY_EDITOR
            UpdateDebugInfo();
#endif
        }

        private void PopFinishedGoals()
        {
            while (goals.Count > 0 && goals.Peek().IsFinished())
                goals.Pop();
        }

        private void PerformSequentialActions()
        {
            int actionsPerformed = 0;
            while (goals.Count > 0 && energy > 0)
            {
                PopFinishedGoals();
                if (goals.Count > 0)
                {
                    Goal goal = goals.Peek();
                    int cost = goal.GetTimeCost();
                    goal.PerformAction();
                    actionsPerformed++;
                    energy -= cost;
                    if (cost <= 0)
                        goto Exit;
                }
            }
            Exit:
            Think($"Performed {actionsPerformed} actions this turn.");
        }

        public void Think(string thought)
        {
            while (thoughts.Count > MAX_THOUGHTS)
                thoughts.RemoveAt(0);
            thoughts.Add(thought);
        }

#if UNITY_EDITOR
        private void UpdateDebugInfo()
        {
            currentGoal = CurrentGoal?.GetType().Name;
            GetCurrentGoalStatus();
            TryUpdatePath();
        }
        
        private void GetCurrentGoalStatus()
        {
            currentGoalStatus = $"Current Goal is null? ({CurrentGoal == null}) | " +
                                $"Current Goal is MoveTo? ({CurrentGoal is MoveTo})";
        }

        private void TryUpdatePath()
        {
            try
            {
                if (CurrentGoal != null && CurrentGoal is MoveTo)
                    UpdatePath((MoveTo)CurrentGoal);
            }
            catch (Exception e)
            {
                throw new BrainException(
                    this, $"{parent.display.GetDisplayName()} could not update path debug info: {e.Message}");
            }
        }

        private void UpdatePath(MoveTo goal) => path = goal.path?.path; 
#endif
    }
}