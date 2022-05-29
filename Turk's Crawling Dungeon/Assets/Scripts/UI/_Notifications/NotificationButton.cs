using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.UI.Notifications
{
    public class NotificationButton 
    {
        public string text;
        public Action onClick = () => { };

        private Func<bool> canClick;

        public NotificationButton(string text)
        {
            this.text = text;
        }

        public NotificationButton(string text, Action onClick, Func<bool> canClick = null)
        {
            this.text = text;
            this.onClick = onClick;
            this.canClick = canClick;
        }

        public bool CanClick()
        {
            if (canClick == null)
                return true;
            return canClick();
        }
    }
}
