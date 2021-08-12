using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts
{
    public class Invulnerable : Part
    {
        public override string Name => "Invulnerable";

        public override bool HandleEvent<T>(T e)
        {
            if (e.Id == BeforeDestroyObjectEvent.id)
                return OnBeforeDestroyObject(e);
            return base.HandleEvent(e);
        }

        private bool OnBeforeDestroyObject(LocalEvent e)
        {
            BeforeDestroyObjectEvent beforeDestroyObjectEvent = (BeforeDestroyObjectEvent) e;
            if (beforeDestroyObjectEvent.obj == parent)
                return false;
            return true;
        }
    }
}
