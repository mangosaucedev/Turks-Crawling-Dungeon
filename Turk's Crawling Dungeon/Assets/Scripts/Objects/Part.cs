using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects
{
    public abstract class Part : MonoBehaviour, ILocalEventHandler 
    {
        public BaseObject parent;

        public abstract string Name { get; }

        public Vector2Int Position => parent.cell.Position;

        public int X => parent.cell.X;
        
        public int Y => parent.cell.Y;

        protected virtual void Awake()
        {
            if (!parent)
                parent = GetComponentInParent<BaseObject>();
        }

        protected virtual void Start()
        {

        }

        protected virtual void OnEnable()
        {

        }

        protected virtual void OnDisable()
        {

        }

        public virtual bool FireEvent<T>(ILocalEventHandler target, T e) where T : LocalEvent =>
            target.HandleEvent(e);

        public virtual bool HandleEvent<T>(T e) where T : LocalEvent
        {
            if (e.Id == GetInteractionsEvent.id)
                GetInteractions(e);
            return true;
        }

        private void GetInteractions(LocalEvent e)
        {
            GetInteractionsEvent getInteractionsEvent = (GetInteractionsEvent)e;
            GetInteractions(getInteractionsEvent);
        }

        protected virtual void GetInteractions(GetInteractionsEvent e)
        {

        }
    }
}
