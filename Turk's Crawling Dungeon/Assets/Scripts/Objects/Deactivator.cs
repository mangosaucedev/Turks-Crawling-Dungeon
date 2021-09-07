using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects
{
    [Serializable]
    public class Deactivator : ObjectComponent, IDeactivator
    {
        private List<object> conditions = new List<object>();
        private bool isDeactivated;

        public BaseObject Parent
        {
            get => parent;
            set => parent = value;
        }

        public GameObject GameObject => Parent?.gameObject ?? null;

        public List<object> Conditions => conditions;

        public bool IsDeactivated => isDeactivated;

        public Deactivator(BaseObject parent) : base(parent)
        {
            this.parent = parent;
        }

        public virtual void AddDeactivatingCondition(object condition)
        {
            RemoveNullConditions();
            if (!conditions.Contains(condition))
                Conditions.Add(condition);
            CheckActivationStatus();
        }

        public virtual void RemoveDeactivatingCondition(object condition)
        {
            RemoveNullConditions();
            if (conditions.Contains(condition))
                Conditions.Remove(condition);
            CheckActivationStatus();
        }

        private void RemoveNullConditions() =>
            Conditions.RemoveAll(c => c == null);

        private void CheckActivationStatus()
        {
            if (Conditions.Count > 0 && !isDeactivated)
                Deactivate();
            else if (Conditions.Count == 0 && isDeactivated)
                Activate();
        }

        public virtual void Activate()
        {
            if (!Parent)
                return;
            isDeactivated = false;
            GameObject?.SetActive(true);
        }

        public virtual void Deactivate()
        {
            if (!Parent)
                return;
            isDeactivated = true;
            GameObject?.SetActive(false);
        }
    }
}
