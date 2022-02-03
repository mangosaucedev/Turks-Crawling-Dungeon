using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace TCD.Texts
{
    public class GameText
    {
        private const string INVALID_PAGE = "INVALID PAGE INDEX";
        private const int FULL_TEXT = -1;

        private static StringBuilder stringBuilder = new StringBuilder();

        public string name;
        public List<string> pages = new List<string>();

        public GameText(params string[] pages)
        {
            foreach (string page in pages)
                this.pages.Add(page);
        }

        public GameText(List<string> pages)
        {
            this.pages = pages;
        }

        public string ToRichText(int page)
        {
            if (pages.Count > page)
                return pages[page];
            return INVALID_PAGE;
        }

        public string ToRichText(int page, int length = FULL_TEXT)
        {
            if (pages.Count > page)
            {
                if (length == FULL_TEXT)
                    return pages[page];
                return pages[page].Substring(0, length);
            }
            return INVALID_PAGE;
        }

        public override string ToString() => AllPagesToRichText(FULL_TEXT);

        public string ToString(int length = FULL_TEXT) => AllPagesToRichText(length);

        private string AllPagesToRichText(int length = FULL_TEXT)
        {
            stringBuilder.Clear();
            for (int i = 0; i < pages.Count; i++)
            {
                string page = pages[i];
                stringBuilder.Append(page);
                if (i < pages.Count - 1)
                    stringBuilder.Append("\n\n");
            }
            if (length == FULL_TEXT)
                return stringBuilder.ToString();
            return stringBuilder.ToString().Substring(0, length);
        }
    }
}
