using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;
using TCD.Objects.Parts;

namespace TCD
{
    public static class SelectionHandler
    {
        private static BaseObject selectedObject;
        private static Cell selectedCell;
        private static Inventory selectedInventory;

        public static BaseObject SelectedObject => selectedObject;

        public static Cell SelectedCell => selectedCell;

        public static Inventory SelectedInventory => selectedInventory;

        public static void SetSelectedObject(BaseObject obj)
        {
            selectedObject = obj;
        }

        public static void SetSelectedCell(Cell cell)
        {
            selectedCell = cell;
        }

        public static void SetSelectedInventory(Inventory inventory)
        {
            selectedInventory = inventory;
        }
    }
}
