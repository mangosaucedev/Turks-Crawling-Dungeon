using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TCD.UI.Notifications
{
    public class NotificationView : ViewController
    {
        [SerializeField] private Text title;
        [SerializeField] private Text message;
        [SerializeField] private GameObject buttonPrefab;
        [SerializeField] private Transform buttonParent;

        private Notification notification;

        protected override string ViewName => gameObject.name;

        protected override void OnEnable()
        {
            base.OnEnable();
            View.CloseEvent += DisplayNext;
        }

        private void DisplayNext()
        {
            View.CloseEvent -= DisplayNext;
            NotificationHandler.DisplayNext();
        }

        public void Display(Notification notification)
        {
            DebugLogger.Log("[Notification]: Opening view for " + notification?.title ?? "Null");

            if (notification == null)
            {
                DebugLogger.LogError("[Notification]: Tried to display null notification! View closing...");
                CloseView();
                return;
            }

            this.notification = notification;
            title.text = notification.title;
            message.text = notification.message;
            if (notification.Buttons > 0)
                BuildButtons();
        }

        private void BuildButtons()
        {
            foreach (NotificationButton button in notification.buttons)
                BuildButton(button);
        }

        private void BuildButton(NotificationButton button)
        {
            GameObject buttonObject = Instantiate(buttonPrefab, buttonParent);
            ViewButton viewButton = buttonObject.GetComponent<ViewButton>();
            viewButton.SetText(button.text);
            viewButton.onClick.AddListener(() => { button.onClick(); });
            viewButton.interactable = button.CanClick();
        }
    }
}
