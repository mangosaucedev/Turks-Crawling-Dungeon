using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.UI;

namespace TCD.Objects.Parts
{
    public class Readable : Part
    {
        [SerializeField] private string text;

        public override string Name => "Readable";

        public string Text
        {
            get => text;
            set => text = value;
        }

        protected override void Start()
        {
            base.Start();
        }

        protected override void GetInteractions(GetInteractionsEvent e)
        {
            base.GetInteractions(e);
            e.interactions.Add(new Interaction("Read", OnRead));
        }

        private void OnRead()
        {
            SelectionHandler.SetSelectedObject(parent);
            ViewManager.Open("Read View");
        }
    }
}
