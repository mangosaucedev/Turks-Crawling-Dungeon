using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts.Talents
{
    public abstract class Talent : Part
    { 
        public int level;
        public bool isActive;
        public int activeCooldown;

        public abstract Sprite Sprite { get; }

        public abstract int MaxLevel { get; }

        public abstract TargetMode TargetMode { get; }

        public abstract UseMode UseMode { get; }

        public abstract IEnumerator OnObjectRoutine(BaseObject obj);

        public abstract IEnumerator OnCellRoutine(Cell cell);

        public abstract string GetDescription();

        public virtual int GetEnergyCost() => TimeInfo.TIME_PER_STANDARD_TURN;

        public virtual int GetCooldown() => 0;

        public virtual int GetRange() => 1;

        public virtual List<TalentRequirement> GetRequirements()
        {
            List<TalentRequirement> requirements = new List<TalentRequirement>();
            return requirements;
        }

        public virtual bool CanUseTalent()
        {
            foreach (TalentRequirement requirement in GetRequirements())
            {
                if (!requirement.MeetsRequirement(parent))
                    return false;
            }
            return true;
        }

        public override bool HandleEvent<T>(T e)
        {
            return base.HandleEvent(e);
        }
    }
}
