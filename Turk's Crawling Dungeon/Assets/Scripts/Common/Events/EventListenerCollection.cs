using System;
using System.Collections.Generic;
using System.Text;

namespace TCD
{
    public class EventListenerCollection
    {
        private Dictionary<object, EventDelegate<Event>> actions =
            new Dictionary<object, EventDelegate<Event>>();
        private List<object> listeners = new List<object>();

        public void AddListener<T>(object listener, EventDelegate<T> action)
            where T : Event
        {
            if (!listeners.Contains(listener))
            {
                listeners.Add(listener);
                actions.Add(listener, e => { });
            }
            EventDelegate<Event> listenerAction = actions[listener];
            listenerAction += e => { action?.Invoke((T) e); };
            actions[listener] = listenerAction;
        }

        public void RemoveListener<T>(object listener)
        {
            if (listeners.Contains(listener))
            {
                listeners.Remove(listener);
                actions.Remove(listener);
            }
        }

        public void Send<T>(T e) where T : Event
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
            { 
                object listener = listeners[i];
                EventDelegate<T> action = actions[listener];
                action?.Invoke(e);
            }
        }
    }
}
