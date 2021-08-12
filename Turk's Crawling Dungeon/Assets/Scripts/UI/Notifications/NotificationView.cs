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
        [SerializeField] private Transform buttonParent;

        protected override string ViewName => gameObject.name;

        protected virtual void Start()
        {
            title.text = NotificationHandler.currentTitle;
            message.text = NotificationHandler.currentMessage;
            SetupButtons();
        }

        protected virtual void SetupButtons()
        {

        }
    }
}
