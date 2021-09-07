using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts
{
    public class Stats : Part
    { 
        [SerializeField] private string initStats;
        [SerializeField] private string statTemplate;

        public string InitStats
        {
            get => initStats;
            set => initStats = value;
        }

        public string StatTemplate
        {
            get => statTemplate;
            set => statTemplate = value;
        }

        public override string Name => "Stats";

        private Dictionary<Stat, int> baseLevel = new Dictionary<Stat, int>();   

        protected override void Start()
        {
            base.Start();
            if (!string.IsNullOrEmpty(StatTemplate))
                LoadStatsFromTemplate();
            else
                ParseInitStats();
        }

        private void LoadStatsFromTemplate()
        {
            throw new NotImplementedException("Method TCD.Objects.Parts.Stats.LoadStatsFromTemplate() is not defined!");
        }

        private void ParseInitStats()
        {
            try
            {
                string[] statStrings = InitStats.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string statString in statStrings)
                    ParseBaseLevelFromStatString(statString);
            }
            catch (Exception e)
            {
                throw new StatsException(
                    this, $"Could not parse initial stats string of {parent.name}: " + e.Message);
            }
        }

        private void ParseBaseLevelFromStatString(string statString)
        {
            string[] splitString = statString.Split(new char[] {':'}, StringSplitOptions.RemoveEmptyEntries);
            string statName = splitString[0];
            int level = int.Parse(splitString[1]);
            Stat stat = (Stat) Enum.Parse(typeof(Stat), statName);
            baseLevel[stat] = level;
        }

        public int GetStatLevel(Stat stat)
        {
            int level = GetStatBaseLevel(stat);
            GetStatEvent getStatEvent = LocalEvent.Get<GetStatEvent>();
            getStatEvent.obj = parent;
            getStatEvent.stat = stat;
            getStatEvent.level = level;
            FireEvent(parent, getStatEvent);
            return getStatEvent.level;
        }

        public int GetStatBaseLevel(Stat stat)
        {
            if (!baseLevel.TryGetValue(stat, out int value))
            {
                value = 0;
                baseLevel[stat] = value;
            }
            return value;
        }

        public int RollStat(Stat stat)
        {
            int max = Mathf.Max(GetStatLevel(stat), 0);
            return RandomInfo.Random.Next(0, max);
        }

        public override bool HandleEvent<T>(T e)
        {
            if (e.Id == GetStatEvent.id)
                OnGetStat(e);
            return base.HandleEvent(e);
        }

        private void OnGetStat(LocalEvent e)
        {
            GetStatEvent getStatEvent = (GetStatEvent) e;
            if (getStatEvent.obj != parent)
                return;
            if (getStatEvent.stat == Stat.Dodge)
                GetDodgeFromStats(getStatEvent);
            if (getStatEvent.stat == Stat.PhysicalSave)
                GetPhysicalSaveFromStats(getStatEvent);
            if (getStatEvent.stat == Stat.MentalSave)
                GetMentalSaveFromStats(getStatEvent);
            if (getStatEvent.stat == Stat.PhysicalPower)
                GetPhysicalPowerFromStats(getStatEvent);
            if (getStatEvent.stat == Stat.MentalPower)
                GetMentalPowerFromStats(getStatEvent);
        }

        private void GetDodgeFromStats(GetStatEvent e)
        {
            int agility = GetStatLevel(Stat.Agility);
            e.level += Mathf.Max(Mathf.FloorToInt((agility - 10) / 2), 0);
        }

        private void GetPhysicalSaveFromStats(GetStatEvent e)
        {
            int constitution = GetStatLevel(Stat.Constitution);
            e.level += Mathf.Max(Mathf.FloorToInt((constitution - 10) * 1.2f), 0);
        }

        private void GetMentalSaveFromStats(GetStatEvent e)
        {
            int willpower = GetStatLevel(Stat.Willpower);
            e.level += Mathf.Max(Mathf.FloorToInt((willpower - 10) * 1.2f), 0);
        }

        private void GetPhysicalPowerFromStats(GetStatEvent e)
        {
            int strength = GetStatLevel(Stat.Strength);
            e.level += Mathf.Max(Mathf.FloorToInt((strength - 10) * 0.8f), 0);
        }

        private void GetMentalPowerFromStats(GetStatEvent e)
        {
            int charm = GetStatLevel(Stat.Charm);
            e.level += Mathf.Max(Mathf.FloorToInt((charm - 10) * 0.8f), 0);
        }
    }
}
