using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;

namespace TCD.UI
{
    public class Inventory_v2Item 
    {
        public BaseObject itemObject;

        private GameObject buttonObject;
        private GameObject weightObject;
        private GameObject xObject;

        public Inventory_v2Item(BaseObject itemObject)
        {
            this.itemObject = itemObject;
        }

        public void AssignObjects(GameObject buttonObject, GameObject weightObject, GameObject xObject = null)
        {
            this.buttonObject = buttonObject;
            this.weightObject = weightObject;
            this.xObject = xObject;
        }

        public void Enable()
        {
            buttonObject?.SetActive(true);
            weightObject?.SetActive(true);
            xObject?.SetActive(true);
        }

        public void Disable()
        {
            buttonObject?.SetActive(false);
            weightObject?.SetActive(false);
            xObject?.SetActive(false);
        }

        public void Destroy()
        {
            if (buttonObject)
                Object.Destroy(buttonObject);
            if (weightObject)
                Object.Destroy(weightObject);
            if (xObject)
                Object.Destroy(xObject);
        }
    }
}
