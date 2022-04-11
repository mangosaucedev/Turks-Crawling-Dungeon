using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;
using TCD.Objects.Attacks;
using TCD.Objects.Parts;
using TCD.Objects.Parts.Effects;
using TCD.TimeManagement;

namespace TCD.Objects.Parts.Talents
{
    public class Lifeblood : Talent
    {
        public override string Name => "Lifeblood";

        public override string TalentTree => "Fitness";

        public override string IconName => "LifebloodIcon";

        public override int MaxLevel => 5;

        public override UseMode UseMode => UseMode.Passive;

        public override TargetMode TargetMode => TargetMode.None;

        protected override bool CanUseOnObject(BaseObject obj) => true;

        public static float GetHealingMultiplier(int level)
        {
            switch (level)
            {
                default:
                    return 10f;
                case 2:
                    return 12.5f;
                case 3:
                    return 15f;
                case 4:
                    return 17.5f;
                case 5:
                    return 20f;
            }
        }

        public override List<ITalentRequirement> GetRequirements(int level)
        {
            var requirements = base.GetRequirements(level);
            requirements.Add(new RequiresTalentLevel(typeof(ThickSkin)));
            return requirements;
        }

        protected override void OnObject()
        {

        }

        protected override bool CanUseOnCell(Cell cell) => true;

        protected override void OnCell()
        {

        }

        public override bool Activate()
        {
            var effects = parent.Parts.Get<Effects.Effects>();
            effects.AddEffect(new SurgingLifeblood(level), Mathf.FloorToInt(GetDuration(level)));
            activeCooldown = GetCooldown(level);
            if (parent == PlayerInfo.currentPlayer)
                MessageLog.Add($"Your lifeblood surges forth, mending your grievous wounds!");
            return base.Activate();
        }

        public override int GetEnergyCost() => 0;

        public override int GetRange(int level) => 1;

        public int GetDuration(int level)
        {
            switch (level)
            {
                default:
                    return 5 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 2:
                    return 5 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 3:
                    return 6 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 4:
                    return 6 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 5:
                    return 7 * TimeInfo.TIME_PER_STANDARD_TURN;
            }
        }

        public override int GetCooldown(int level)
        {
            switch (level)
            {
                default:
                    return 32 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 2:
                    return 29 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 3:
                    return 26 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 4:
                    return 23 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 5:
                    return 20 * TimeInfo.TIME_PER_STANDARD_TURN;
            }
        }

        public override string GetDescription(int level) => $"On reaching below 30% health, your health regeneration will " +
            $"increase {GetHealingMultiplier(level) * 100}% for {((float) GetDuration(level) / TimeInfo.TIME_PER_STANDARD_TURN).RoundToDecimal(1)} " +
            $"turns.";

        public override bool HandleEvent<T>(T e)
        {
            if (e.Id == HpModifiedEvent.id)
                OnHpModified();
            return base.HandleEvent(e);
        }

        private void OnHpModified()
        {
            DebugLogger.Log("LIFEBLOOD: hp modified!");

            if (!parent.Parts.TryGet(out Resources resources) || parent.Parts.TryGet(out Effects.Effects effects))
                return;

            float hpMax = resources.GetMaxResource(Resource.Hitpoints);
            float hp = resources.GetResource(Resource.Hitpoints);
            float percent = (float) hp / (float) hpMax;

            bool belowThreshold = percent < 0.3f;
            bool cooldownActive = activeCooldown > 0f;
            bool hasEffect = effects.HasEffect("Surging Lifeblood");

            if (belowThreshold)
                DebugLogger.Log("LIFEBLOOD: hp below threshold!");
            if (!cooldownActive)
                DebugLogger.Log("LIFEBLOOD: cooldown inactive!");
            if (!hasEffect)
                DebugLogger.Log("LIFEBLOOD: does not have 'Surging Lifeblood' effect active!");

            if (belowThreshold && !cooldownActive && !hasEffect)
                Activate();
        }
    }
}
