using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TCD.Objects.Parts
{
    [Serializable]
    public class Faction 
    {
        private const int MIN_RELATION = -1000;
        private const int MAX_RELATION = 1000;

        public string name;
        public string displayName;
        public FactionRelation[] relations;

        public int GetRelation(string name)
        {
            FactionRelation relation = FindRelation(name);
            if (relation == null)
            {
                Faction faction = Assets.Get<Faction>(name);
                relation = faction.FindRelation(this.name);
                if (relation == null)
                {
                    SetRelation(name, 0);
                    faction.SetRelation(this.name, 0);
                    return 0;
                }
                SetRelation(name, relation.relation);
                return relation.relation;
            }
            else
                return relation.relation;
        }

        public FactionRelation FindRelation(string name)
        {
            if (relations == null)
                return null;
            foreach (FactionRelation relation in relations)
            {
                if (relation.faction == name)
                    return relation;
            }
            return null;
        }

        public void SetRelation(string name, int amount)
        {
            FactionRelation relation = FindRelation(name);
            if (relation == null)
            {
                relation = new FactionRelation { faction = name, relation = amount };
                if (this.relations != null)
                {
                    List<FactionRelation> relations = this.relations.ToList();
                    relations.Add(relation);
                    this.relations = relations.ToArray();
                }
                else
                    this.relations = new FactionRelation[] { relation };
            }
            else
            {
                relation.faction = name;
                relation.relation = amount;
            }
        }

        public void ModifyRelation(string name, int amount)
        {
            int currentRelation = GetRelation(name);
            FactionRelation relation = FindRelation(name);
            if (relation == null)
                SetRelation(name, amount);
            else
                relation.relation = Mathf.Clamp(currentRelation + amount, MIN_RELATION, MAX_RELATION);
        }

        public FactionOutlook GetOutlook(string name)
        {
            int relation = GetRelation(name);
            Array values = Enum.GetValues(typeof(FactionOutlook));
            FactionOutlook outlook = FactionOutlook.Revered;
            foreach (FactionOutlook o in values)
            {
                if (relation <= (int) o && o < outlook)
                    outlook = o;
            }
            return outlook;
        }

        public bool IsFriendly(string name) => GetOutlook(name) >= FactionOutlook.Appreciated;

        public bool IsAggressive(string name) => GetOutlook(name) <= FactionOutlook.Disapproving;
    }
}
