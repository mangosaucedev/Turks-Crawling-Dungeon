using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TCD.UI.Tooltips
{
    public class Tooltip : MonoBehaviour
    {
        private const int CHARACTER_WRAP_LIMIT = 64;

        public Text header;
        public Text body;
        
        [SerializeField] private LayoutElement layoutElement;

        private void Update()
        {
            int headerLength = header.text.Length;
            int bodyLength = body.text.Length;
            bool layoutElementEnabled = headerLength > CHARACTER_WRAP_LIMIT || bodyLength > CHARACTER_WRAP_LIMIT;
            layoutElement.enabled = layoutElementEnabled;
        }
    }
}
