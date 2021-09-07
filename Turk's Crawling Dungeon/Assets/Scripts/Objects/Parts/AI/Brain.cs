using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts
{
    public class Brain : Part
    {
        private const int MAX_THOUGHTS = 32;
        private const int MAX_ACTIONS_PER_TURN = 64;

        [Header("AI Parameters")]
        [SerializeField] private int sightRadius;
        [SerializeField] private string faction;

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

        public string Faction
        {
            get => faction;
            set => faction = value;
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
            int energySpent = 0;
            int i = 0;
            while (goals.Count > 0 && energy > 0)
            {
                if (goals.Count > 0)
                {
                    Goal goal = goals.Peek();
                    int cost = goal.GetTimeCost();
                    bool successful = goal.PerformAction();
                    actionsPerformed++;
                    energy -= cost;
                    energySpent += cost;
                    if (!successful)
                        break;
                }
                PopFinishedGoals();
                i++;
                if (i > MAX_ACTIONS_PER_TURN)
                {
                    DebugLogger.LogError($"Brain of {parent.GetDisplayName()} caught in endless loop!");
                    break;
                }
            }
            if (energy > 0)
                energy = 0;
            Think($"Performed {actionsPerformed} actions this turn for {energySpent} energy.");
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
                    this, $"{parent.GetDisplayName()} could not update path debug info: {e.Message}");
            }
        }

        private void UpdatePath(MoveTo goal) => path = goal.path?.path; 
#endif
    }
}