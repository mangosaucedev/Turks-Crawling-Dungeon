using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace TCD.UI
{
    public class ViewButton : Selectable, IElement, IPointerClickHandler, IEventSystemHandler, ISubmitHandler
    {
        [SerializeField] private View view;
        [SerializeField] private Text text;

        public UnityEvent onClick;

        public string ViewName
        {
            get => view.gameObject.name;
        }

        public View View
        {
            get
            {
                if (!view)
                    view = GetComponentInParent<View>();
                return view;
            }
        }

        protected override void Awake()
        {
            base.Awake();

            if (!text)
                text = GetComponentInChildren<Text>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            View.SetActiveEvent += OnSetActive;
            View.UpdateEvent += OnSetInactive;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            View.SetActiveEvent -= OnSetActive;
            View.UpdateEvent -= OnSetInactive;
        }

        public static T Create<T>(string prefabName, Transform parent) where T : ViewButton
        {
            return Create<T>(Assets.Get<GameObject>(prefabName), parent);
        }

        public static T Create<T>(GameObject prefab, Transform parent) where T : ViewButton
        {
            GameObject gameObject = Instantiate(prefab, parent);
            return gameObject.GetComponent<T>();
        }

        public virtual void OnSetActive() => interactable = IsInteractive();
        

        public virtual void OnSetInactive() => interactable = false;
        

        protected virtual bool IsInteractive()
        {
            return true;
        }

        public virtual void OnPointerClick(PointerEventData data)
        {
            if (interactable)
                onClick?.Invoke();
        }

        public virtual void OnSubmit(BaseEventData data)
        {
            if (interactable)
                onClick?.Invoke();
        }

        public void SetText(string str)
        {
            text.text = str;
        }
    }
}