using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects
{
    public interface IDeactivator
    {
        List<object> Conditions { get; }

        bool IsDeactivated { get; }

        public void AddDeactivatingCondition(object condition);

        public void RemoveDeactivatingCondition(object condition);

        void Activate();

        void Deactivate();
    }
}
