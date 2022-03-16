using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TCD.Objects;
using TCD.Objects.Parts;
using TCD.Texts;

namespace TCD.UI
{
    public class ReadView : ViewController
    {
        [SerializeField] private Text title;
        [SerializeField] private Text body;
        [SerializeField] private Text pageCount;
        [SerializeField] private ViewButton previousButton;
        [SerializeField] private ViewButton nextButton;
        private BaseObject obj;
        private GameText currentText;

        protected override string ViewName => gameObject.name;

        private void Start()
        {   
            obj = SelectionHandler.SelectedObject;
            Readable readable = obj.parts.Get<Readable>();
            currentText = Assets.Get<GameText>(readable.Text);
            title.text = $"/ Reading {obj.GetDisplayName()} /";
            GoToPage(0);
        }

        private void GoToPage(int page)
        {

        }
    }
}
