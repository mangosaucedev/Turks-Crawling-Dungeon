using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using UnityEngine;
using TCD.Objects.Parts;

namespace TCD.Pathfinding
{
    public class PathfindingManager : MonoBehaviour
    {
        private static BlockingCollection<AstarPathEntry> inputPaths = new BlockingCollection<AstarPathEntry>();

        public static void AddPathEntry(AstarPathEntry entry) => inputPaths.Add(entry);

        public void OutputPaths()
        {
            inputPaths.CompleteAdding();
            foreach (AstarPathEntry entry in inputPaths)
            {
                if (GoalUtility.TryGetFromGuid(entry.id, out MoveTo moveTo))
                    moveTo.nativePath = entry.path;
            }

            inputPaths.Dispose();
            inputPaths = new BlockingCollection<AstarPathEntry>();
        }
    }
}
