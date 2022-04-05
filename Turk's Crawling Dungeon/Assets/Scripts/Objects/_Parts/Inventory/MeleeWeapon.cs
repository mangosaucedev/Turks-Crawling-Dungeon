using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using TCD.Objects.Attacks;

namespace TCD.Objects.Parts
{
    [Serializable]
    public class MeleeWeapon : Equippable
    {
        public List<Attack> attacks = new List<Attack>();

        [SerializeField] private string initAttacks;

        public string InitAttacks
        {
            get => initAttacks;
            set => initAttacks = value;
        }

        public override string Name => "Melee Weapon";

        protected override void Start()
        {
            base.Start();
            if (!string.IsNullOrEmpty(InitAttacks))
                ParseAttacks();
        }

        private void ParseAttacks()
        {
            try
            {
                string[] attackStrings = InitAttacks.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string attackString in attackStrings)
                    ParseAttackAndWeight(attackString);
            }
            catch (Exception e)
            {
                ExceptionHandler.Handle(
                    new Exception($"Could not parse initial melee weapon attacks string of {parent.name}: " + e.Message));
            }
        }

        private void ParseAttackAndWeight(string attackString)
        {
            string[] splitString = attackString.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
            string attackName = splitString[0];
            float weight = float.Parse(splitString[1], CultureInfo.InvariantCulture);
            Attack attack = AttackFactory.BuildFromBlueprint(attackName);
            attack.weapon = parent;
            attack.weight = weight;
            attacks.Add(attack);
        }

        public override bool HandleEvent<T>(T e)
        {
            if (e.Id == GetAttacksEvent.id && IsEquipped)
                OnGetAttacks(e);
            if (e.Id == GetDescriptionEvent.id)
                OnGetDescription(e);
            return base.HandleEvent(e);
        }

        private void OnGetAttacks(LocalEvent e)
        {
            GetAttacksEvent getAttacks = (GetAttacksEvent) e;
            OnGetAttacks(getAttacks);
        }

        private void OnGetAttacks(GetAttacksEvent e)
        {
            bool isInOffhand = IsInOffhand();
            foreach (Attack attack in attacks)
            {
                float weight = attack.weight;
                if (isInOffhand)
                    weight *= 0.3f;
                e.AddAttack(attack, weight);
            }
        }

        public bool IsInOffhand()
        {
            if (!IsEquipped || RequiresSecondSlot)
                return false;
            if (equipment.GetEquippedItem(EquipSlot.LeftHand))
                return true;
            return false;
        }

        private void OnGetDescription(LocalEvent e)
        {
            GetDescriptionEvent getDescriptionEvent = (GetDescriptionEvent)e;
            if (getDescriptionEvent.Object != parent)
                return;
            getDescriptionEvent.AddToPrefix($"<i><color=#039be5>Melee Damage:</color> {GetMinDamage()}-{GetMaxDamage()}</i>");
            getDescriptionEvent.AddToPrefix($"<i><color=#039be5>Melee Damage Types:</color> {GetDamageTypesString()}</i>");
            if (RequiresSecondSlot)
                getDescriptionEvent.AddToPrefix($"<i><color=#039be5>Requires both hands to use.</color></i>");
        }

        public float GetMinDamage()
        {
            float minDamage = 9999;
            foreach (Attack attack in attacks)
            {
                if (attack.minDamage < minDamage)
                    minDamage = attack.minDamage;
            }
            return minDamage;
        }

        public float GetMaxDamage()
        {
            float maxDamage = 0;
            foreach (Attack attack in attacks)
            {
                if (attack.maxDamage > maxDamage)
                    maxDamage = attack.maxDamage;
            }
            return maxDamage;
        }

        private string GetDamageTypesString()
        {
            string damageTypes = "";
            List<DamageType> listedDamageTypes = new List<DamageType>();
            foreach (Attack attack in attacks)
            {
                if (!listedDamageTypes.Contains(attack.damageType))
                {
                    if (damageTypes.Length > 0)
                        damageTypes += " / ";
                    damageTypes += attack.damageTypeName;
                    listedDamageTypes.Add(attack.damageType);
                }
            }
            return damageTypes;
        }
    }
}
