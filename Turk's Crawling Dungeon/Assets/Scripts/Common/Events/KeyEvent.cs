using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Inputs;

namespace TCD
{
    public class KeyEvent : Event
    {
        private static List<KeyEvent> pool = new List<KeyEvent>();

        public KeyEventContext context;

        public override string Name => "Key";

        public KeyEvent() : base()
        {

        }

        public KeyEvent(KeyEventContext context) : this()
        {
            this.context = context;
        }

        ~KeyEvent()
        {
            context = KeyEventContext.none;
            if (!pool.Contains(this))
                pool.Add(this);
        }

        public static KeyEvent FromPool(KeyEventContext context)
        {
            KeyEvent e = null;
            try
            {
                if (pool.Count > 0)
                {
                    e = pool[0];
                    if (e != null)
                    {
                        e.context = context;
                        if (pool.Contains(e))
                            pool.Remove(e);
                        return e;
                    }
                }
            }
            catch
            {
                return new KeyEvent(context);
            }
            return new KeyEvent(context);
        }
    }
}
