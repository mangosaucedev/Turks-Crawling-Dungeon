using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Parts;

namespace TCD.Objects
{
    [Serializable]
    public class BaseObject : MonoBehaviour, ILocalEventHandler
    {
        public CellObject cell;
        public Deactivator deactivator;
        public PartCollection parts;
        public Transform partsParent;
        [SerializeField] private SpriteRenderer spriteRenderer;

        public SpriteRenderer SpriteRenderer
        {
            get
            {
                if (!spriteRenderer)
                    spriteRenderer = GetComponentInChildren<SpriteRenderer>();
                return spriteRenderer;
            }
        }

        protected virtual void Awake()
        {
            cell = new CellObject(this); 
            deactivator = new Deactivator(this);
            parts = new PartCollection(this);
            if (!partsParent)
                FindPartsParentTransform();
        }

        private void FindPartsParentTransform()
        {
            foreach (Transform child in transform)
            {
                if (child.name == "Parts")
                {
                    partsParent = child;
                    return;
                }
            }
            throw new ObjectException($"Could not find parent transform for object '{name}'!");
        }

        public bool FireEvent<T>(ILocalEventHandler target, T e) where T : LocalEvent =>
            target.HandleEvent(e);

        public bool HandleEvent<T>(T e) where T : LocalEvent
        {
            bool isSuccessful = true;
            foreach (Part part in parts.Parts)
            {
                if (!part.HandleEvent(e))
                    isSuccessful = false;
            }
            return isSuccessful;
        }

        public bool Destroy()
        {
            BeforeDestroyObjectEvent beforeDestroyObjectEvent = LocalEvent.Get<BeforeDestroyObjectEvent>();
            beforeDestroyObjectEvent.obj = this;
            if (!HandleEvent(beforeDestroyObjectEvent))
                return false;
            DestroyObjectEvent destroyObjectEvent = LocalEvent.Get<DestroyObjectEvent>();
            destroyObjectEvent.obj = this;
            HandleEvent(destroyObjectEvent);
            cell.SetPosition(0, 0);
            Destroy(gameObject);
            return true;
        }

        public string GetDescription()
        {
            GetDescriptionEvent e = GetDescriptionEvent();
            return e.GetDescription();
        }

        private GetDescriptionEvent GetDescriptionEvent()
        {
            GetDescriptionEvent e = LocalEvent.Get<GetDescriptionEvent>();
            e.obj = this;
            Render render = parts.Get<Render>();
            e.description = render.Description;
            HandleEvent(e);
            return e;
        }

        public string GetDescriptionShort()
        {
            GetDescriptionEvent e = GetDescriptionEvent();
            return e.GetDescriptionShort();
        }

        public string GetDisplayName()
        {
            GetDisplayNameEvent e = GetDisplayNameEvent();
            return e.GetDisplayName();
        }

        private GetDisplayNameEvent GetDisplayNameEvent()
        {
            GetDisplayNameEvent e = LocalEvent.Get<GetDisplayNameEvent>();
            e.obj = this;
            Render render = parts.Get<Render>();
            e.displayName = render.DisplayName;
            e.displayNamePlural = render.DisplayNamePlural;
            HandleEvent(e);
            return e;
        }

        public string GetDisplayNamePlural()
        {
            GetDisplayNameEvent e = GetDisplayNameEvent();
            return e.GetDisplayNamePlural();
        }
    }
}
