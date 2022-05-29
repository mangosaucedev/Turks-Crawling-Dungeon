using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using TCD.Objects.Parts;

namespace TCD.Pathfinding
{
    public struct AstarPath : IDisposable
    {
        private const int MAX_STEPS = 64 * 64;
        private const int MAX_LENGTH = 32 * 32;
        private const int MAX_HEAP_SIZE = 256 * 256;
        private const int DIAGONAL_MOVE_COST = 14;
        private const int MOVE_COST = 10;

        public Vector2Int startPosition;
        public Vector2Int targetPosition;
        public NativeList<Vector2Int> nativePath;
        public bool isValid;
        public int stepsToCreate;
        public bool isInitialized;

        private AstarGrid grid;
        private AstarNode startNode;
        private AstarNode targetNode;
        private AstarNode currentNode;
        private NativeHeap<AstarNodeCost> openSet;
        private NativeList<Vector2Int> closedSet;
        private NativeHashMap<Vector2Int, AstarNodeCost> costs;

        public AstarPath(MoveToPathfindingJob job, Allocator allocator)
        {
            grid = job.parameters.grid;
            startPosition = job.parameters.startPosition;
            targetPosition = job.parameters.targetPosition;
            
            isInitialized = true;
            nativePath = new NativeList<Vector2Int>(MAX_LENGTH, allocator);
            isValid = false;
            stepsToCreate = 0;
            openSet = new NativeHeap<AstarNodeCost>(MAX_HEAP_SIZE, allocator);
            closedSet = new NativeList<Vector2Int>(MAX_HEAP_SIZE, allocator);
            costs = new NativeHashMap<Vector2Int, AstarNodeCost>(MAX_HEAP_SIZE, allocator);

            startNode = grid.Get(startPosition.x, startPosition.y);
            currentNode = startNode;
            openSet.Add(new AstarNodeCost(startPosition));

            if (startPosition == targetPosition)
            {
                targetNode = startNode;
                isValid = true;
            }
            else
            {
                targetNode = grid.Get(targetPosition.x, targetPosition.y);
                isValid = true;//TryCalculatePath();
            }

            openSet.Dispose();
            closedSet.Dispose();
            costs.Dispose();
        }

        public static implicit operator NativeList<Vector2Int>(AstarPath path) => path.nativePath;

        public void Initialize()
        {
            isInitialized = true;
            nativePath = new NativeList<Vector2Int>(1, Allocator.TempJob);
            grid.grid = new NativeArray<AstarNode>(1, Allocator.TempJob);
            openSet = new NativeHeap<AstarNodeCost>(MAX_HEAP_SIZE, Allocator.TempJob);
            closedSet = new NativeList<Vector2Int>(MAX_HEAP_SIZE, Allocator.TempJob);
            costs = new NativeHashMap<Vector2Int, AstarNodeCost>(MAX_HEAP_SIZE, Allocator.TempJob);
        }

        private bool TryCalculatePath()
        {
            if (!CanPathToTargetPosition())
                return false;

            while (openSet.Count > 0)
            {
                var position = openSet.RemoveFirst().position;
                currentNode = grid.Get(position.x, position.y);
                closedSet.Add(new Vector2Int(currentNode.x, currentNode.y));

                if (currentNode.Equals(targetNode))
                    return RetracePath();

                EvaluateCurrentNodeNeighbors();
                stepsToCreate++;
                if (stepsToCreate > MAX_STEPS)
                    return false;
            }
            return false;
        }

        private bool CanPathToTargetPosition() =>
            (grid.IsWithin(startPosition.x, startPosition.y) && grid.IsWithin(targetPosition.x, targetPosition.y));

        private void EvaluateCurrentNodeNeighbors()
        {
            using (var neighbors = grid.GetNeighbors(currentNode))
            {
                foreach (AstarNode node in neighbors)
                {
                    if (NodeCanBeEvaluated(node))
                        EvaluateNode(node);
                    else
                        closedSet.Add(new Vector2Int(node.x, node.y));
                }
            }
        }

        private bool NodeCanBeEvaluated(AstarNode node) =>
            (node.isPassable
            && !closedSet.Contains(node.Position))
            || node.Equals(startNode)
            || node.Equals(targetNode);

        private void EvaluateNode(AstarNode node)
        {
            if (!costs.TryGetValue(node.Position, out var cost))
            {
                cost = new AstarNodeCost(node.Position);
                costs[node.Position] = cost;
            }

            int moveToNodeCost = cost.gCost +
                                 GetDistanceCost(currentNode, node) +
                                 node.difficulty;

            bool isInOpenSet = openSet.Contains(cost);
            if (moveToNodeCost < cost.gCost || !isInOpenSet)
            {
                cost.gCost = moveToNodeCost;
                cost.hCost = GetDistanceCost(node, targetNode);
                cost.parent = currentNode.Position;

                if (!isInOpenSet)
                    openSet.Add(cost);
            }
        }

        private int GetDistanceCost(AstarNode a, AstarNode b)
        {
            int xDistance = Mathf.Abs(a.x - b.x);
            int yDistance = Mathf.Abs(a.y - b.y);

            if (xDistance > yDistance)
                return (DIAGONAL_MOVE_COST * yDistance) +
                       (MOVE_COST * (xDistance - yDistance));
            return (DIAGONAL_MOVE_COST * xDistance) +
                   (MOVE_COST * (yDistance - xDistance));
        }

        private bool RetracePath()
        {
            currentNode = targetNode;

            int length = 0;

            while (!currentNode.Equals(startNode))
            {
                nativePath.Add(currentNode.Position);

                var cost = costs[currentNode.Position];
                currentNode = grid.Get(cost.parent.x, cost.parent.y);

                length++;

                if (length > MAX_LENGTH)
                    return false;
            }

            NativeList<Vector2Int> finalPath = new NativeList<Vector2Int>(MAX_LENGTH, Allocator.Temp);

            int index = 0;
            for (int i = nativePath.Length - 1; i >= 0; i--)
            {
                finalPath[index] = nativePath[i];
                index++;
            }
            nativePath.Dispose();
            nativePath = finalPath;

            return true;
        }

        public bool TryToGetIndexOfPosition(Vector2Int position, out int index)
        {
            index = -1;
            for (int i = 0; i < nativePath.Length; i++)
            {
                Vector2Int checkPosition = nativePath[i];
                if (checkPosition == position)
                {
                    index = i;
                    return true;
                }
            }
            return false;
        }

        public void Dispose()
        { 
            //if (nativePath.IsCreated)
            //   nativePath.Dispose();
        }
    }
}
