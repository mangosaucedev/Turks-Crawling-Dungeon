using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.UI
{
    public class TabController : MonoBehaviour
    {
        [SerializeField] private List<GameObject> tabs = new List<GameObject>();

        public void OpenTab(GameObject gameObject)
        {
            tabs.ForEach(o => o.SetActive(false));
            gameObject.SetActive(true);
        }
    }
}
