using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace TCD.UI
{
    public class MessageLogDisplay : MonoBehaviour
    {
        [SerializeField] private Text text;
        private StringBuilder stringBuilder = new StringBuilder();
        private List<string> messages;

        private void Start()
        {
            text.text = "";
        }

        private void OnEnable()
        {
            EventManager.Listen<MessageLogUpdatedEvent>(this, OnMessageLogUpdated);
        }

        private void OnDisable()
        {
            EventManager.StopListening<MessageLogUpdatedEvent>(this);   
        }

        private void OnMessageLogUpdated(MessageLogUpdatedEvent e)
        {
            messages = MessageLog.messages;
            BuildMessageLog();
            text.text = stringBuilder.ToString();
            while (text.preferredHeight > text.rectTransform.rect.height)
            {
                messages.RemoveAt(0);
                BuildMessageLog();
                text.text = stringBuilder.ToString();
            }
        }

        private void BuildMessageLog()
        {
            stringBuilder.Clear();
            foreach (string message in messages)
            {
                stringBuilder.Append(message);
                stringBuilder.Append("\n");
            }
        }
    }
}
