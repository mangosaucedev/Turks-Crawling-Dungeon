using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Attacks;

namespace TCD.Objects.Parts
{
    public class StandardCutInjury : StandardInjury
    {
        private int DEPTH_FAT = 30;
        private int DEPTH_MUSCLE = 50;
        private int DEPTH_NERVE = 70;
        private int DEPTH_BONE = 85;

        private string name;
        private string description;
        private int bleed;
        private int depth;

        public StandardCutInjury(Attack attack) : base(attack)
        {

        }

        protected override void LoadAttack(Attack attack)
        {
            if (attack.weapon == null)
                LoadUnarmedAttack(attack);
            else
                LoadWeaponAttack(attack);
        }

        private void LoadUnarmedAttack(Attack attack)
        {
            
        }

        private void LoadWeaponAttack(Attack attack)
        {

        }

        public override void OnAcquire()
        { 
            
        }

        public override void OnHealed()
        {

        }

        public override float GetSeverity()
        {
            return 0f;
        }

        public override string GetName() => name;

        public override string GetDescription() => description;
    }
}
