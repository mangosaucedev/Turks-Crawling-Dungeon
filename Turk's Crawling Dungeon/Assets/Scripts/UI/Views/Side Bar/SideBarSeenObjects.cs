using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TCD.Objects;
using TCD.Objects.Parts;

namespace TCD.UI
{
    public class SideBarSeenObjects : MonoBehaviour
    {
        [SerializeField] private Text seenCharacters;
        [SerializeField] private Text seenItems;
        [SerializeField] private Text seenOther;
        private List<BaseObject> allSeenObjects = new List<BaseObject>();

        private void Start()
        {
            seenCharacters.text = "";
            seenItems.text = "";
            seenOther.text = "";
        }

        private void OnEnable()
        {
            EventManager.Listen<AfterTurnTickEvent>(this, OnAfterTurnTick);
        }

        private void OnDisable()
        {
            EventManager.StopListening<AfterTurnTickEvent>(this);
        }

        private void OnAfterTurnTick(AfterTurnTickEvent e) => UpdateSeenObjects();

        private void UpdateSeenObjects()
        {
            GetAllSeenObjects();
            UpdateSeenCharacters();
            UpdateSeenItems();
            UpdateSeenOther();
        }

        private void GetAllSeenObjects()
        {
            allSeenObjects.Clear();
            GameGrid grid = CurrentZoneInfo.grid;
            foreach (Vector2Int position in FieldOfView.visiblePositions)
            {
                Cell cell = grid[position];
                AddAllSeenObjectsInCell(cell);
            }
        }

        private void AddAllSeenObjectsInCell(Cell cell)
        {
            foreach (BaseObject obj in cell.Objects)
            {
                if (obj != PlayerInfo.currentPlayer && obj.Parts.TryGet(out Visible visible) && visible.IsVisibleToPlayer())
                    allSeenObjects.Add(obj);
            }
        }

        private void UpdateSeenCharacters()
        {
            List<BaseObject> allSeenCharacters = ExtractObjectsWithPart<Brain>();
            SeenObjectsFormatter formatter = new SeenObjectsFormatter(allSeenCharacters);
            seenCharacters.text = formatter.GetText();
        }

        private List<BaseObject> ExtractObjectsWithPart<T>() where T : Part
        {
            List<BaseObject> allSeenWithPart = new List<BaseObject>();
            for (int i = allSeenObjects.Count - 1; i >= 0; i--)
            {
                BaseObject obj = allSeenObjects[i];
                if (obj.Parts.TryGet(out T part))
                {
                    allSeenObjects.RemoveAt(i);
                    allSeenWithPart.Add(obj);
                }    
            }
            return allSeenWithPart;
        }

        private void UpdateSeenItems()
        {
            List<BaseObject> allSeenItems = ExtractObjectsWithPart<Item>();
            SeenObjectsFormatter formatter = new SeenObjectsFormatter(allSeenItems);
            seenItems.text = formatter.GetText();
        }

        private void UpdateSeenOther()
        {
            SeenObjectsFormatter formatter = new SeenObjectsFormatter(allSeenObjects);
            seenOther.text = formatter.GetText();
        }
    }
}
