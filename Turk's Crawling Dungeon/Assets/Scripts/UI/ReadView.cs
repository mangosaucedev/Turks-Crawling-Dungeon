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
            title.text = $"/ Reading {obj.display.GetDisplayName()} /";
            GoToPage(0);
        }

        private void GoToPage(int page)
        {
            body.text = currentText.pages[page];
            int pages = currentText.pages.Count;
            if (page > 0)
                previousButton.onClick.AddListener(() => { GoToPage(page - 1); });
            else
                previousButton.onClick.AddListener(() => { GoToPage(0); });
            if (page < pages - 1)
                nextButton.onClick.AddListener(() => { GoToPage(page + 1); });
            else
                nextButton.onClick.AddListener(() => { GoToPage(pages - 1); });
            pageCount.text = $"Page {page + 1} / {pages}";
        }
    }
}
