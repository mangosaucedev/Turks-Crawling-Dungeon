using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Resources = TCD.Objects.Parts.Resources;

namespace TCD.Objects.Parts.Talents
{
    public abstract class Talent : Part
    { 
        public int level = 1;
        public bool isActive;
        public int activeCooldown;

        public int Level
        {
            get => level;
            set => level = value;
        }

        public abstract string TalentTree { get; }

        public abstract Sprite Icon { get; }

        public abstract int MaxLevel { get; }

        public virtual Resource Resource => Resource.Stamina;

        public abstract TargetMode TargetMode { get; }

        public abstract UseMode UseMode { get; }

        public virtual string Indicator { get; }

        protected override void OnEnable()
        {
            base.OnEnable();
            EventManager.Listen<BeforeTurnTickEvent>(this, OnBeforeTurnTick);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            EventManager.StopListening<BeforeTurnTickEvent>(this);
        }

        protected virtual void OnBeforeTurnTick(BeforeTurnTickEvent e)
        {
            if (!isActive && activeCooldown >= 0)
                activeCooldown -= e.timeElapsed;
            if (isActive && parent.parts.TryGet(out Resources resources))
            {
                float timeMultiplier = (float) e.timeElapsed / TimeInfo.TIME_PER_STANDARD_TURN;
                float sustainCost = -GetSustainResourceCost() * timeMultiplier;
                if (resources.GetResource(Resource) <= Mathf.Abs(sustainCost))
                    Deactivate();
                resources.ModifyResource(Resource, sustainCost);
            }
        }

        public abstract IEnumerator OnObjectRoutine(BaseObject obj);

        public abstract IEnumerator OnCellRoutine(Cell cell);

        public virtual bool Activate()
        {
            if (parent.parts.TryGet(out Resources resources))
                resources.ModifyResource(Resource, -GetActivationResourceCost());
            isActive = true;
            return true;
        }

        public virtual bool Deactivate()
        {
            isActive = false;
            activeCooldown = GetCooldown();
            return false;
        }

        public abstract string GetDescription();

        public virtual int GetEnergyCost() => TimeInfo.TIME_PER_STANDARD_TURN;

        public virtual int GetActivationResourceCost() => 0;

        public virtual int GetSustainResourceCost() => 0;

        public virtual int GetCooldown() => 0;

        public virtual int GetRange() => 1;

        public virtual List<TalentRequirement> GetRequirements()
        {
            List<TalentRequirement> requirements = new List<TalentRequirement>();
            return requirements;
        }

        public virtual bool CanUseTalent()
        {
            if ((parent.parts.TryGet(out Resources resources) && resources.GetResource(Resource) < GetActivationResourceCost()) || 
                activeCooldown >= 0)
                return false;
            foreach (TalentRequirement requirement in GetRequirements())
            {
                if (!requirement.MeetsRequirement(parent))
                    return false;
            }
            return true;
        }

        public override bool HandleEvent<T>(T e)
        {
            if (e.Id == AIBeforeAttackEvent.id)
                return OnAIBeforeAttack(e);
            if (e.Id == AIBeforeMoveEvent.id)
                return OnAIBeforeMove(e);
            if (e.Id == AttackEvent.id)
                return OnAttack(e);
            return base.HandleEvent(e);
        }
        
        private bool OnAIBeforeAttack(LocalEvent e)
        {
            AIBeforeAttackEvent aiBeforeAttackEvent = (AIBeforeAttackEvent) e;
            return OnAIBeforeAttack(aiBeforeAttackEvent);
        }

        protected virtual bool OnAIBeforeAttack(AIBeforeAttackEvent e)
        {
            return true;
        }

        private bool OnAIBeforeMove(LocalEvent e)
        {
            AIBeforeMoveEvent aiBeforeMoveEvent = (AIBeforeMoveEvent) e;
            return OnAIBeforeMove(aiBeforeMoveEvent);
        }

        protected virtual bool OnAIBeforeMove(AIBeforeMoveEvent e)
        {
            return true;
        }

        private bool OnAttack(LocalEvent e)
        {
            AttackEvent attackEvent = (AttackEvent) e;
            return OnAttack(attackEvent);
        }

        protected virtual bool OnAttack(AttackEvent e)
        {
            return true;
        }
    }
}
