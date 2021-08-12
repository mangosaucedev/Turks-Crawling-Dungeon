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
            if (pool.Count > 0)
            {
                KeyEvent e = pool[0];
                if (e != null)
                {
                    e.context = context;
                    pool.RemoveAt(0);
                    return e;
                }
            }
            return new KeyEvent(context);
        }
    }
}
