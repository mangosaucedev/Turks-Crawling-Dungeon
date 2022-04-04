using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using TCD.Objects;

namespace TCD.UI
{
    public class SeenObjectsFormatter
    {
        private List<BaseObject> objects;
        private StringBuilder stringBuilder = new StringBuilder();
        private List<string> names = new List<string>();
        private Dictionary<string, int> nameCounts = new Dictionary<string, int>();

        public SeenObjectsFormatter(List<BaseObject> objects)
        {
            this.objects = objects;
            SortList();
            CountNames();
        }

        private void SortList()
        {
            BaseObject player = PlayerInfo.currentPlayer;
            Vector2Int playerPosition = player.cell.Position;
            objects.OrderBy(o => Vector2Int.Distance(o.cell.Position, playerPosition));
        }

        private void CountNames()
        {
            foreach (BaseObject obj in objects)
                AddName(obj.GetDisplayName());
        }

        private void AddName(string name)
        {
            if (!names.Contains(name))
            {
                names.Add(name);
                nameCounts[name] = 1;
            }
            else
                nameCounts[name]++;
        }

        public string GetText()
        {
            foreach (string name in names)
            {
                if (stringBuilder.Length > 0)
                    stringBuilder.Append(", ");
                stringBuilder.Append(name);
                int count = nameCounts[name];
                if (count > 1)
                {
                    stringBuilder.Append(" (x");
                    stringBuilder.Append(count);
                    stringBuilder.Append(')');
                } 
            }
            return stringBuilder.ToString();
        }
    }
}
