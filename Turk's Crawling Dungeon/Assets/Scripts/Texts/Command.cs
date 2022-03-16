using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Texts
{
    public abstract class Command
    {
        public string[] arguments;
        public int startIndex;
        public int endIndex;
        public string targetText;
        public string unhandledText;

        public abstract CommandType Type { get; }

        public int Length => targetText.Length;

        public void HandleText(ref string text)
        {
            unhandledText = text;
            ApplyCommandToTargetText(ref text);
            targetText = text;
        }

        protected abstract void ApplyCommandToTargetText(ref string text);
    }
}
