using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;
using TCD.Objects.Attacks;

namespace TCD.Objects.Parts
{
    public class Hazard : Part
    {
        [SerializeField] private string attackName;
        
        private Attack attack;

        public string Attack
        {
            get => attackName;
            set => attackName = value;
        }

        public override string Name => "Hazard";

        protected override void Start()
        {
            base.Start();
            attack = AttackFactory.BuildFromBlueprint(Attack);
            attack.weapon = parent;
            attack.weight = 1;
            attack.hitAccuracy = 9999;
        }

        public override bool HandleEvent<T>(T e)
        {
            if (e.Id == EnteredCellEvent.id)
                OnEnteredCell(e);
            if (e.Id == GetDescriptionEvent.id)
                OnGetDescription(e);
            return base.HandleEvent(e);
        }

        private void OnEnteredCell(LocalEvent e)
        {
            EnteredCellEvent enteredCellEvent = (EnteredCellEvent) e;
            OnEnteredCell(enteredCellEvent);
        }

        protected virtual void OnEnteredCell(EnteredCellEvent e)
        {
            if (!e.Object.parts.TryGet(out Movement movement))
                return;
            AttackHandler.AttackTarget(parent, e.Object, attack);
        }

        protected virtual void OnGetDescription(LocalEvent e)
        {
            GetDescriptionEvent getDescriptionEvent = (GetDescriptionEvent)e;
            getDescriptionEvent.AddToSuffix("<i><color=red>Hazardous to navigate.</color></i>");
        }
    }
}
