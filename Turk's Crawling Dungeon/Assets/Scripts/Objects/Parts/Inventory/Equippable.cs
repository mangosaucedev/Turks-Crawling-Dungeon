using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts
{
    public abstract class Equippable : Part 
    {
        public Equipment equipment;
        public EquipSlot[] equipSlots;

        [SerializeField] private string slot;
        [SerializeField] private string offSlot;
        [SerializeField] private bool requiresSecondSlot;
        [SerializeField] private string stats;

        private EquipSlot primarySlot;
        private EquipSlot secondarySlot;
        private Dictionary<Stat, int> boostedStatsByLevel = new Dictionary<Stat, int>();
        private List<Stat> boostedStats = new List<Stat>();

        public string Slot
        {
            get => slot;
            set => slot = value;
        }

        public string OffSlot
        {
            get => offSlot;
            set => offSlot = value;
        }

        public bool RequiresSecondSlot
        {
            get => requiresSecondSlot;
            set => requiresSecondSlot = value;
        }

        public string Stats
        {
            get => stats;
            set => stats = value;
        }

        public bool IsEquipped => equipment;

        public BaseObject Owner => equipment?.parent;

        public bool IsEquippedToPlayer => Owner != null && Owner == PlayerInfo.currentPlayer;

        protected override void Start()
        {
            base.Start();
            if (Enum.TryParse(Slot, out EquipSlot primarySlot))
                this.primarySlot = primarySlot;
            if (Enum.TryParse(OffSlot, out EquipSlot secondarySlot))
                this.secondarySlot = secondarySlot;
            equipSlots = (this.secondarySlot > EquipSlot.None) ? 
                new EquipSlot[] { primarySlot, secondarySlot } : 
                new EquipSlot[] { primarySlot };

            if (!string.IsNullOrEmpty(Stats))
                ParseStats();
        }

        private void ParseStats()
        {
            string[] splitString = Stats.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string str in splitString)
            {
                string[] statAndLevel = str.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                string statName = statAndLevel[0];
                Stat stat = (Stat)Enum.Parse(typeof(Stat), statName);
                int level = int.Parse(statAndLevel[1]);
                BoostStat(stat, level);
            }
        }

        public void BoostStat(Stat stat, int level)
        {
            boostedStats.Add(stat);
            if (!boostedStatsByLevel.ContainsKey(stat))
                boostedStatsByLevel[stat] = level;
            else 
                boostedStatsByLevel[stat] += level;
        }

        protected override void GetInteractions(GetInteractionsEvent e)
        {
            base.GetInteractions(e);
            if (!IsEquippedToPlayer)
                e.interactions.Add(new Interaction("Equip", OnPlayerEquip));
            else
                e.interactions.Add(new Interaction("Unequip", OnPlayerUnequip));
        }

        private void OnPlayerEquip()
        {
            BaseObject player = PlayerInfo.currentPlayer;
            Equipment playerEquipment = player.parts.Get<Equipment>();
            if (playerEquipment.TryEquip(parent))
            {
                EquippedEvent e = LocalEvent.Get<EquippedEvent>();
                e.obj = parent;
                FireEvent(parent, e);
            }
        }

        private void OnPlayerUnequip()
        {
            BaseObject player = PlayerInfo.currentPlayer;
            Equipment playerEquipment = player.parts.Get<Equipment>();
            if (playerEquipment.TryUnequip(parent))
            {
                UnequippedEvent e = LocalEvent.Get<UnequippedEvent>();
                e.obj = parent;
                FireEvent(parent, e);
            }
        }

        public override bool HandleEvent<T>(T e)
        {
            if (e.Id == GetStatEvent.id && IsEquipped)
                OnGetStat(e);
            return base.HandleEvent(e);
        }

        private void OnGetStat(LocalEvent e)
        {
            GetStatEvent getStatEvent = (GetStatEvent)e;
            OnGetStat(getStatEvent);
        }

        protected virtual void OnGetStat(GetStatEvent e)
        {
            if (boostedStats.Contains(e.stat))
                e.level += boostedStatsByLevel[e.stat];
        }
    }
}
