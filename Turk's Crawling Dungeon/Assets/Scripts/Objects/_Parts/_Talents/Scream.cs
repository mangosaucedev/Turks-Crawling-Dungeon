using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Parts.Effects;
using TCD.TimeManagement;

namespace TCD.Objects.Parts.Talents
{
    public class Scream : Talent
    {
        public override string Name => "Scream";

        public override string TalentTree => "StrangeTechniques";

        public override string IconName => "ScreamIcon";

        public override int MaxLevel => 5;

        public override UseMode UseMode => UseMode.Activated;

        public override TargetMode TargetMode => TargetMode.None;

        protected override bool CanUseOnObject(BaseObject obj) => true;

        protected override void OnObject() => ScreamInRadius();

        protected override bool CanUseOnCell(Cell cell) => true;

        protected override void OnCell() => ScreamInRadius();

        private void ScreamInRadius()
        {

        }

        public int GetRadius(int level)
        {
            switch (level)
            {
                default:
                    return 3;
                case 2:
                    return 4;
                case 3:
                    return 5;
                case 4:
                    return 6;
                case 5:
                    return 7;
            }
        }

        public float GetDamage(int level)
        {
            switch (level)
            {
                default:
                    return 40f;
                case 2:
                    return 60f;
                case 3:
                    return 80f;
                case 4:
                    return 95f;
                case 5:
                    return 110f;
            }
        }

        public override int GetEnergyCost() => TimeInfo.TIME_PER_STANDARD_TURN;

        public override int GetRange(int level) => 1;

        public override int GetCooldown(int level)
        {
            switch (level)
            {
                default:
                    return 14 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 2:
                    return 13 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 3:
                    return 12 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 4:
                    return 10 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 5:
                    return 8 * TimeInfo.TIME_PER_STANDARD_TURN;
            }
        }

        public override string GetDescription(int level) => $"Unleash an ear-bursting cry that " +
            $"deals {GetDamage(level)} pure damage to enemies in a radius of {GetRadius(level)}.";
    }
}
