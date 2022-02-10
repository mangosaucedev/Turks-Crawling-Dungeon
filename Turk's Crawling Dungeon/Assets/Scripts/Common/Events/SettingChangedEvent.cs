using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Settings;

namespace TCD
{
    public class SettingChangedEvent : Event
    {
        private static List<SettingChangedEvent> pool = new List<SettingChangedEvent>();

        public SettingsEntry entry;
        public object previousValue;

        public override string Name => "Setting Changed Event";

        public SettingChangedEvent(SettingsEntry entry, object previousValue) : base()
        {
            this.entry = entry;
            this.previousValue = previousValue;
        }

        ~SettingChangedEvent()
        {
            entry = null;
            previousValue = null;
            if (!pool.Contains(this))
                pool.Add(this);
        }

        public static SettingChangedEvent FromPool(SettingsEntry entry, object previousValue)
        {
            SettingChangedEvent e = null;
            try
            {
                if (pool.Count > 0)
                {
                    e = pool[0];
                    if (e != null)
                    {
                        e.entry = entry;
                        e.previousValue = previousValue;
                        if (pool.Contains(e))
                            pool.Remove(e);
                        return e;
                    }
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleMessage($"Setting Changed Event could not be retrieved: " + exception.Message);
            }
            return new SettingChangedEvent(entry, previousValue);
        }
    }
}
