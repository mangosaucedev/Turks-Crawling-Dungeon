using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TCD.Objects;

namespace TCD.UI
{
    public class InspectView : ViewController
    {
        [SerializeField] private Text title;
        [SerializeField] private Text description;
        private BaseObject obj;

        protected override string ViewName => gameObject.name;

        private void Start()
        {
            obj = SelectionHandler.SelectedObject;
            title.text = $"/ Inspecting {obj.GetDisplayName()} /";
            description.text = obj.GetDescription();
        }
    }
}
