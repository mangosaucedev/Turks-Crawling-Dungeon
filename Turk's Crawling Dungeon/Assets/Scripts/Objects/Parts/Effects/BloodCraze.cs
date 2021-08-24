using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TCD.Objects.Parts.Effects
{
    public class BloodCraze : Effect
    {
        public int physicalPowerBonus;
        public float attackCostMultiplier;

        public override string Name => "Blood-Craze";

        public override Sprite Icon => Assets.Get<Sprite>("BloodCrazedIcon");

        public override EffectStacking Stacking => EffectStacking.RefreshCooldown;

        public BloodCraze(int physicalPowerBonus, float attackCostMultiplier)
        {
            this.physicalPowerBonus = physicalPowerBonus;
            this.attackCostMultiplier = attackCostMultiplier;
        }

        public override string GetDescription() => $"While Blood-Crazed, your physical power is " +
            $"increased by {physicalPowerBonus} and your attack speed is increased by {1 - attackCostMultiplier}%.";

        public override bool HandleEvent<T>(T e)
        {
            if (e.Id == GetStatEvent.id)
                OnGetStat(e);
            return base.HandleEvent(e);
        }

        private void OnGetStat(LocalEvent e)
        {
            GetStatEvent getStatEvent = (GetStatEvent)e;
            if (getStatEvent.stat == Stat.AttackCost)
                getStatEvent.level = Mathf.CeilToInt(getStatEvent.level * attackCostMultiplier);
            if (getStatEvent.stat == Stat.PhysicalPower)
                getStatEvent.level += physicalPowerBonus;
        }
    }
}
