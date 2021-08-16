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
        public string key;
        public UnityEvent onClick;

        [SerializeField] private View view;
        [SerializeField] private Text keyText;
        [SerializeField] private Text text;

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

        protected override void Start()
        {
            base.Start();
            if (!string.IsNullOrEmpty(key))
                keyText.text = key + ")";
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

        protected virtual void Update()
        {
            CheckIfKeyPressed();
        }

        public static T Create<T>(string prefabName, Transform parent, string key = "") 
            where T : ViewButton
        {
            return Create<T>(Assets.Get<GameObject>(prefabName), parent, key);
        }

        public static T Create<T>(GameObject prefab, Transform parent, string key = "") 
            where T : ViewButton
        {
            GameObject gameObject = Instantiate(prefab, parent);
            T button = gameObject.GetComponent<T>();
            button.key = key;
            return button;
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

        private void CheckIfKeyPressed()
        {
            if (string.IsNullOrEmpty(key))
                return;
            if (ButtonInputKeys.IsKeyDown(key) && interactable)
                onClick?.Invoke();
        }
    }
}