using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Attacks;

namespace TCD.Objects.Parts
{
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
                throw new Exception($"Could not parse initial melee weapon attacks string of {parent.name}: " + e.Message);
            }
        }

        private void ParseAttackAndWeight(string attackString)
        {
            string[] splitString = attackString.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
            string attackName = splitString[0];
            float weight = float.Parse(splitString[1]);
            Attack attack = AttackFactory.BuildFromBlueprint(attackName);
            attack.weapon = parent;
            attack.weight = weight;
            attacks.Add(attack);
        }

        public override bool HandleEvent<T>(T e)
        {
            if (e.Id == GetAttacksEvent.id && IsEquipped)
                OnGetAttacks(e);
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
    }
}
