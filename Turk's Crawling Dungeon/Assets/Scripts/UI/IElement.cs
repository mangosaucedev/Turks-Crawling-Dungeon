using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.UI
{
    public interface IElement
    {
        string ViewName { get; }

        View View { get; }

        void OnSetActive();

        void OnSetInactive();
    }
}
