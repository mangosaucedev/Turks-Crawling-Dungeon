using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Parts.Effects;
using TCD.TimeManagement;

namespace TCD.Objects.Parts.Talents
{
    [Serializable]
    public class Scream : Talent
    {
        public override string Name => "Scream";

        public override string TalentTree => "StrangeTechniques";

        public override Sprite Icon => Assets.Get<Sprite>("ScreamIcon");

        public override int MaxLevel => 5;

        public override UseMode UseMode => UseMode.Activated;

        public override TargetMode TargetMode => TargetMode.None;

        public override IEnumerator OnObjectRoutine(BaseObject obj)
        {
            ScreamInRadius();
            yield break;
        }

        public override IEnumerator OnCellRoutine(Cell cell)
        {
            ScreamInRadius();
            yield break;
        }

        private void ScreamInRadius()
        {

        }

        public int GetRadius()
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

        public float GetDamage()
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

        public override int GetRange() => 1;

        public override int GetCooldown()
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

        public override string GetDescription() => $"Unleash an ear-bursting cry that " +
            $"deals {GetDamage()} pure damage to enemies in a radius of {GetRadius()}.";
    }
}
