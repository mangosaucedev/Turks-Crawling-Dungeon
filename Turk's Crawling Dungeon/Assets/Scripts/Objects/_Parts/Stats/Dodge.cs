using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Attacks;

namespace TCD.Objects.Parts
{
    [Serializable]
    public class Dodge : Statistic
    {
        public override string Name => "Dodge";
     
        protected override Stat Stat => Stat.Dodge;

        public override bool HandleEvent<T>(T e)
        {
            if (e.Id == BeforeAttackEvent.id)
                return OnBeforeAttack(e);
            return base.HandleEvent(e);
        }

        private bool OnBeforeAttack(LocalEvent e)
        {
            BeforeAttackEvent beforeAttackEvent = (BeforeAttackEvent) e;
            if (!beforeAttackEvent.target == parent)
                return true;
            Attack attack = beforeAttackEvent.attack;
            if (attack.damageType.undodgeable || !parent.parts.TryGet(out Stats stats))
                return true;
            int hitAccuracy = attack.hitAccuracy;
            int dodge = stats.RollStat(Stat);
            if (dodge > hitAccuracy)
            {
                beforeAttackEvent.SetResult("was dodged");
                return false;
            }
            return true;
        }

        protected override void OnGetStat(GetStatEvent e)
        {
            base.OnGetStat(e);
            e.baseLevel += BaseLevel;
        }
    }
}
