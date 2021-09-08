using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts
{
    [Serializable]
    public class DropsCorpse : Part
    {
        [SerializeField] private string corpse;

        public string Corpse
        {
            get => corpse;
            set => corpse = value;
        }

        public override string Name => "Drops Corpse";

        public override bool HandleEvent<T>(T e)
        {
            if (e.Id == DestroyObjectEvent.id)
            {
                DestroyObjectEvent destroyObjectEvent = (DestroyObjectEvent) (LocalEvent) e;
                if (destroyObjectEvent.obj == parent)
                    DropCorpse();
            }
            return base.HandleEvent(e);
        }

        private void DropCorpse()
        {
            if (string.IsNullOrEmpty(Corpse))
                return;
            GameObject corpsePrefab = Assets.Get<GameObject>(Corpse);
            GameObject corpseObject = ObjectFactory.BuildFromPrefab(corpsePrefab, Position).gameObject;
        }
    }
}
