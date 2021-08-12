using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects
{
    public class DisplayInfo : ObjectComponent, IDisplayInfo
    {
        public DisplayInfo(BaseObject parent) : base(parent)
        {
            
        }

        public string GetDescription()
        {
            GetDescriptionEvent e = GetDescriptionEvent();
            return e.GetDescription();
        }

        private GetDescriptionEvent GetDescriptionEvent()
        {
            GetDescriptionEvent e = LocalEvent.Get<GetDescriptionEvent>();
            e.obj = parent;
            e.description = parent.description;
            parent.HandleEvent(e);
            return e;
        }

        public string GetDescriptionShort()
        {
            GetDescriptionEvent e = GetDescriptionEvent();
            return e.GetDescriptionShort();
        }

        public string GetDisplayName()
        {
            GetDisplayNameEvent e = GetDisplayNameEvent();
            return e.GetDisplayName();
        }

        private GetDisplayNameEvent GetDisplayNameEvent()
        {
            GetDisplayNameEvent e = LocalEvent.Get<GetDisplayNameEvent>();
            e.obj = parent;
            e.displayName = parent.displayName;
            e.displayNamePlural = parent.displayNamePlural;
            parent.HandleEvent(e);
            return e;
        }

        public string GetDisplayNamePlural()
        {
            GetDisplayNameEvent e = GetDisplayNameEvent();
            return e.GetDisplayNamePlural();
        }
    }
}
