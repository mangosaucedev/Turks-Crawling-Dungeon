using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TCD.UI.Tooltips
{
    public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler   
    {
        [SerializeField] private string header;
        [SerializeField, Multiline] private string body;

        public void OnPointerEnter(PointerEventData data)
        {
            TooltipHandler.Show(GetHeader(), GetBody());
        }

        public void OnPointerExit(PointerEventData data)
        {
            TooltipHandler.Hide();
        }

        public virtual string GetHeader()
        {
            return header;
        }

        public virtual string GetBody()
        {
            return body;
        }
    }
}
