using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts
{
    public class Wearable : Equippable
    {
        [SerializeField] private int armor;
        [SerializeField] private int dodge;
       
        public int Armor
        {
            get => armor;
            set => armor = value;
        }

        public int Dodge
        {
            get => dodge;
            set => dodge = value;
        }

        public override string Name => "Wearable";

        protected override void OnGetStat(GetStatEvent e)
        {
            base.OnGetStat(e);
            if (e.stat == Stat.Armor)
                e.level += Armor;
            if (e.stat == Stat.Dodge)
                e.level += Dodge;
        }
    }
}
