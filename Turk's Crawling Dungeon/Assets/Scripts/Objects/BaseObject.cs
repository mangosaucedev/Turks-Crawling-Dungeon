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
        public static HashSet<BaseObject> allObjects = new HashSet<BaseObject>();
        public static HashSet<BaseObject> enabledObjects = new HashSet<BaseObject>();

        public Guid id = Guid.NewGuid();
        public CellObject cell;
        public Deactivator deactivator;
        public Transform partsParent;
        public LocalVarCollection localVars = new LocalVarCollection();

        [SerializeField] private SpriteRenderer spriteRenderer;

#if UNITY_EDITOR
        [Header("Debug Info")]
        [SerializeField] private Vector2Int position;
#endif

        private PartCollection parts;

        public SpriteRenderer SpriteRenderer
        {
            get
            {
                if (!spriteRenderer)
                    spriteRenderer = GetComponentInChildren<SpriteRenderer>();
                return spriteRenderer;
            }
        }

        public PartCollection Parts
        {
            get
            {
                if (parts == null)
                    parts = new PartCollection(this);
                return parts;
            }
        }

        protected virtual void Awake()
        {
            cell = new CellObject(this); 
            deactivator = new Deactivator(this);
            if (!partsParent)
                FindPartsParentTransform();
            allObjects.Add(this);
        }

        private void Start()
        {
            Parts.Initialize();
        }

        private void OnEnable()
        {
            ObjectUtility.Add(this);
            enabledObjects.Add(this);
#if UNITY_EDITOR
            cell.SetPositionEvent += c => { position = c.Position; };
#endif
        }

        private void OnDisable()
        {
            ObjectUtility.Remove(id);
            enabledObjects.Remove(this);
#if UNITY_EDITOR
            cell.SetPositionEvent -= c => { position = c.Position; };
#endif
        }

        private void OnDestroy()
        {
            allObjects.Remove(this);
        }

        private void FindPartsParentTransform()
        {
            partsParent = transform.Find("Parts");
            if (!partsParent)
                ExceptionHandler.Handle(new ObjectException($"Could not find parent transform for object '{name}'!"));
        }

        public bool FireEvent<T>(ILocalEventHandler target, T e) where T : LocalEvent =>
            target.HandleEvent(e);

        public bool HandleEvent<T>(T e) where T : LocalEvent
        {
            bool isSuccessful = true;
            foreach (Part part in Parts.Parts)
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
            Render render = Parts.Get<Render>();
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
            Render render = Parts.Get<Render>();
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
