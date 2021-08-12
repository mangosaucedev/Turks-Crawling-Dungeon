using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class MessageLog : MonoBehaviour
    {
        private const int MAX_MESSAGES = 50;
        private const int MAX_MESSAGES_IN_STACK = 98;
        private const string MESSAGE_TAG = "> ";

        public static List<string> messages = new List<string>();

        public static int LastIndex
        {
            get
            {
                return messages.Count - 1;
            }
        }

        public static void Add(string message)
        {
            message = MESSAGE_TAG + message;

            if (LastIndex > -1)
            {
                string lastMessage = messages[LastIndex];

                if (lastMessage.Contains("(x"))
                {
                    int index = lastMessage.LastIndexOf(" (x");
                    string substring = lastMessage.Substring(index);
                    lastMessage = lastMessage.Remove(index);

                    if (lastMessage == message)
                    {
                        string countString = "";

                        foreach (char c in substring)
                        {
                            if (char.IsDigit(c))
                                countString += c;
                        }

                        int count = int.Parse(countString);

                        if (count > MAX_MESSAGES_IN_STACK)
                            goto LogMessage;

                        count += 1;

                        messages[LastIndex] = lastMessage + $" (x{count})";
                        EventManager.Send(new MessageLogUpdatedEvent());
                        return;
                    }
                }
                else if (lastMessage == message)
                {
                    messages[LastIndex] = lastMessage + " (x2)";
                    EventManager.Send(new MessageLogUpdatedEvent());
                    return;
                }
            }

        LogMessage:
            messages.Add(message);
            while (messages.Count > MAX_MESSAGES)
                messages.RemoveAt(0);
            EventManager.Send(new MessageLogUpdatedEvent());
        }
    }
}
