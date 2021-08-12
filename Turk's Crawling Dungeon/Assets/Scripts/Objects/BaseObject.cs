using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects
{
    public class BaseObject : MonoBehaviour, ILocalEventHandler
    {
        public string displayName;
        public string displayNamePlural;
        public string description;
        public string faction;
        public float hp;
        public float hpMax;
        public float value;
        public ICellObject cell;
        public IDeactivator deactivator;
        public IDisplayInfo display;
        public IHp health;
        public IPartCollection parts;
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
            display = new DisplayInfo(this);
            health = new Hp(this);
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
    }
}
