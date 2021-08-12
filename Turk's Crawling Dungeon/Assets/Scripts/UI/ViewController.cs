using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.UI
{
    public abstract class ViewController : MonoBehaviour
    {
        private View view;

        protected View View
        {
            get
            {
                if (!view)
                    view = GetComponent<View>();
                return view;
            }
        }

        protected abstract string ViewName { get; }

        protected void CloseView() =>
            ViewManager.Close(ViewName);

        protected virtual void Awake()
        {

        }

        protected virtual void OnEnable()
        {

        }

        protected virtual void OnDisable()
        {

        }
    }
}
