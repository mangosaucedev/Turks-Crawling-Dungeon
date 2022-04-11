using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Graphics;
using TCD.TimeManagement;
using Resources = TCD.Objects.Parts.Resources;

namespace TCD.Objects.Parts.Talents
{
    [Serializable]
    public abstract class Talent : Part
    { 
        public int level = 1;
        public bool isActive;
        public int activeCooldown;

        protected BaseObject target;
        protected Cell targetCell;

        public int Level
        {
            get => level;
            set => level = value;
        }

        public Sprite Icon => SpriteUtility.GetSprite(IconName);

        public abstract string TalentTree { get; }

        public abstract string IconName { get; }

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
            if (isActive && parent.Parts.TryGet(out Resources resources))
            {
                float timeMultiplier = (float) e.timeElapsed / TimeInfo.TIME_PER_STANDARD_TURN;
                float sustainCost = -GetSustainResourceCost(level) * timeMultiplier;
                if (resources.GetResource(Resource) <= Mathf.Abs(sustainCost))
                    Deactivate();
                resources.ModifyResource(Resource, sustainCost);
            }
        }

        public bool OnObject(BaseObject obj)
        {
            if (CanUseOnObject(obj))
            {
                target = obj;
                ActionScheduler.EnqueueAction(parent, OnObject);
                return true;
            }
            return false;
        }

        public bool OnCell(Cell cell)
        {
            if (CanUseOnCell(cell))
            {
                targetCell = cell;
                ActionScheduler.EnqueueAction(parent, OnCell);
                return true;
            }
            return false;
        }

        protected abstract bool CanUseOnObject(BaseObject obj);

        protected abstract void OnObject();

        protected abstract bool CanUseOnCell(Cell cell);

        protected abstract void OnCell();

        public virtual bool Activate()
        {
            if (parent.Parts.TryGet(out Resources resources))
                resources.ModifyResource(Resource, -GetActivationResourceCost(level));
            isActive = true;
            return true;
        }

        public virtual bool Deactivate()
        {
            isActive = false;
            activeCooldown = GetCooldown(level);
            return false;
        }

        public abstract string GetDescription(int level);

        public virtual int GetEnergyCost() => TimeInfo.TIME_PER_STANDARD_TURN;

        public virtual int GetActivationResourceCost(int level) => 0;

        public virtual int GetSustainResourceCost(int level) => 0;

        public virtual int GetCooldown(int level) => 0;

        public virtual int GetRange(int level) => 1;

        public virtual List<ITalentRequirement> GetRequirements(int level)
        {
            List<ITalentRequirement> requirements = new List<ITalentRequirement>();
            return requirements;
        }

        public virtual bool MeetsRequirements(int level)
        {
            foreach (ITalentRequirement requirement in GetRequirements(level))
            {
                if (!requirement.MeetsRequirement())
                    return false;
            }
            return true;
        }
        public virtual bool MeetsEmbarkRequirements(int level)
        {
            foreach (ITalentRequirement requirement in GetRequirements(level))
            {
                if (!requirement.MeetsEmbarkRequirement())
                    return false;
            }
            return true;
        }

        public virtual bool CanUseTalent()
        {
            if ((parent.Parts.TryGet(out Resources resources) && resources.GetResource(Resource) < GetActivationResourceCost(level)) || 
                activeCooldown >= 0)
                return false;
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
