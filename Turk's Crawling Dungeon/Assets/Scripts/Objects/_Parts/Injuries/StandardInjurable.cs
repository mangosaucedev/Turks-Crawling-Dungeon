using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Attacks;

namespace TCD.Objects.Parts
{
    public class StandardInjurable : Part
    {
        [SerializeField] private float injuryDamagePercentThreshold = 0.08f;
        [SerializeField] private float percentChanceOfInjury = 10f;

        public override string Name => "Standard Injurable";

        public float InjuryDamageThreshold
        {
            get => injuryDamagePercentThreshold;
            set => injuryDamagePercentThreshold = value;
        }

        public float PercentChanceOfInjury
        {
            get => percentChanceOfInjury;
            set => percentChanceOfInjury = value;
        }

        public override bool HandleEvent<T>(T e)
        {
            if (e.Id == AttackedEvent.id)
                OnAttacked((AttackedEvent)(LocalEvent)e);
            return base.HandleEvent(e);
        }

        private void OnAttacked(AttackedEvent e)
        {
            if (!parent.Parts.TryGet(out Resources resources))
            {
                ExceptionHandler.HandleMessage($"StandardInjurable part on {parent.name} could not access Resources part!");
                return;
            }
            if (!parent.Parts.TryGet(out Stats stats))
            {
                ExceptionHandler.HandleMessage($"StandardInjurable part on {parent.name} could not access Stats part!");
                return;
            }

            float maxHp = resources.GetMaxResource(Resource.Hitpoints);

            if (e.damage < maxHp * Mathf.Clamp01(injuryDamagePercentThreshold))
                return;

            double roll = 100 * RandomInfo.Random.NextDouble(); 
            if (roll + stats.RollStat(Stat.PhysicalSave) < PercentChanceOfInjury)
                InflictRandomInjury(e);
        }

        private void InflictRandomInjury(AttackedEvent e)
        {
            if (!parent.Parts.TryGet(out Injuries injuries))
            {
                ExceptionHandler.HandleMessage($"StandardInjurable part on {parent.name} could not access Injuries part!");
                return;
            }
            
            switch (e.attack.damageType.name)
            {
                case "Cut":
                    injuries.AddInjury(new StandardCutInjury(e.attack));
                    return;
                case "Pierce":
                    injuries.AddInjury(new StandardPierceInjury(e.attack));
                    return;
                case "Blunt":
                    injuries.AddInjury(new StandardBluntInjury(e.attack));
                    return;
                default:
                    injuries.AddInjury(new StandardDefaultInjury(e.attack));
                    return;
            }
        }
    }
}
