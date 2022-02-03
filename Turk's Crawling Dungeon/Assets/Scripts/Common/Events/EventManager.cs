using System;
using System.Collections.Generic;
using System.Text;

namespace TCD
{
    public static class EventManager
    {
        private static Dictionary<Type, EventListenerCollection> listeners = 
            new Dictionary<Type, EventListenerCollection>();
        private static Dictionary<Type, EventOneShotActionCollection> oneShotActions = 
            new Dictionary<Type, EventOneShotActionCollection>();

        public static void Send<T>(T e) where T : Event
        {
            Type type = typeof(T);
            if (listeners.TryGetValue(type, out EventListenerCollection collection))
            {
                collection.Send(e);
                PerformOneShotActions(type);
            }
        }

        public static void Listen<T>(
            object listener, EventDelegate<T> action) where T : Event
        {
            Type type = typeof(T);
            EventListenerCollection collection = 
                GetEventListenerCollection(type);
            collection.AddListener(listener, action);
        }

        private static EventListenerCollection GetEventListenerCollection(
            Type type)
        {
            if (!listeners.TryGetValue(
                type, out EventListenerCollection collection))
            {
                collection = new EventListenerCollection();
                listeners[type] = collection;
            }
            return collection;
        }

        public static void StopListening<T>(object listener) where T : Event
        {
            Type type = typeof(T);
            EventListenerCollection collection =
                GetEventListenerCollection(type);
            collection.RemoveListener<T>(listener);
        }

        private static void PerformOneShotActions(Type type)
        {
            EventOneShotActionCollection oneShotActions = GetOneShotActionCollection(type);
            oneShotActions.PerformOneShotActions();
        }

        public static void AddOneShotAction<T>(object listener, Action action) where T : Event
        {
            Type type = typeof(T);
            EventOneShotActionCollection oneShotActions = GetOneShotActionCollection(type);
            oneShotActions.AddOneShotAction(listener, action);
        }

        private static EventOneShotActionCollection GetOneShotActionCollection(Type type) 
        {
            if (!oneShotActions.TryGetValue(type, out var actions))
            {
                actions = new EventOneShotActionCollection();
                oneShotActions[type] = actions;
            }
            return actions;
        }
    }
}
