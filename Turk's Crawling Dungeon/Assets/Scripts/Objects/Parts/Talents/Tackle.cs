using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.TimeManagement;

namespace TCD.Objects.Parts.Talents
{
    public class Tackle : Talent
    {
        public override string Name => "Tackle";

        public override Sprite Sprite => Assets.Get<Sprite>("TackleIcon");

        public override int MaxLevel => 5;

        public override UseMode UseMode => UseMode.Activated;

        public override TargetMode TargetMode => TargetMode.Object;

        public override int GetCooldown()
        {
            switch (level)
            {
                default:
                    return 10 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 2: 
                    return 9 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 3:
                    return 8 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 4:
                    return 7 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 5:
                    return 6 * TimeInfo.TIME_PER_STANDARD_TURN;
            }
        }

        public override IEnumerator OnObjectRoutine(BaseObject obj)
        {
            yield break;
        }

        public override IEnumerator OnCellRoutine(Cell cell)
        {
            yield break;
        }

        public override int GetEnergyCost() => TimeInfo.TIME_PER_STANDARD_TURN;

        public override int GetRange()
        {
            switch (level)
            {
                default:
                    return 5;
                case 2:
                    return 6;
                case 3:
                    return 6;
                case 4:
                    return 7;
                case 5:
                    return 8;
            }
        }

        public override string GetDescription() => $"Rush an opponent up to {GetRange()} cells away and try to " +
            $"tackle them to the ground. The enemy must make a saving throw against your physical power or be knocked prone. " +
            $"If they resist your tackle, you are thrown off-balance for 2 turns, reducing your physical save by 10.";
    }
}
