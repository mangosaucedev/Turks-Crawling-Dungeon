using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Pathfinding
{
    public class NavAstarPath
    {
        private const int MAX_STEPS = 2048;
        private const int MAX_LENGTH = 512;
        private const int MAX_HEAP_SIZE = 256 * 256;
        private const int DIAGONAL_MOVE_COST = 14;
        private const int MOVE_COST = 10;

        public List<Vector2Int> path = new List<Vector2Int>();
        public bool isValid;
        public int stepsToCreate;
        private Vector2Int startPosition;
        private Vector2Int targetPosition;
        private NavNode startNode;
        private NavNode targetNode;
        private NavNode currentNode;
        private Heap<NavNode> openSet = new Heap<NavNode>(MAX_HEAP_SIZE);
        private HashSet<NavNode> closedSet = new HashSet<NavNode>();
        private INavEvaluator evaluator;

        public NavAstarPath(Vector2Int startPosition, Vector2Int targetPosition, INavEvaluator evaluator)
        {
            this.startPosition = startPosition;
            this.targetPosition = targetPosition;
            this.evaluator = evaluator;

            if (startPosition == targetPosition)
            {
                path.Add(startPosition);
                isValid = true;
            }
            else if (CanPathToTargetPosition())
                isValid = TryToCalculatePath();
        } 

        private bool CanPathToTargetPosition() =>
            (NavGrid.Current.IsWithinBounds(startPosition)) &&
            (NavGrid.Current.IsWithinBounds(targetPosition));

        private bool TryToCalculatePath()
        {
            startNode = NavGrid.Current[startPosition];
            startNode.ResetCost();
            targetNode = NavGrid.Current[targetPosition];
            targetNode.ResetCost();
            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                currentNode = openSet.RemoveFirst();
                closedSet.Add(currentNode);

                if (currentNode == targetNode)
                    return RetracePath();

                EvaluateCurrentNodeNeighbors();
                stepsToCreate++;
                if (stepsToCreate > MAX_STEPS)
                    return false;
            }
            return false;
        }

        private void EvaluateCurrentNodeNeighbors()
        {
            foreach (NavNode node in currentNode.GetNeighbors())
            {
                if (NodeCanBeEvaluated(node))
                    EvaluateNode(node);
                else
                    closedSet.Add(node);
            } 
        }

        private bool NodeCanBeEvaluated(NavNode node) => 
            (evaluator.IsPassable(node) 
            && !closedSet.Contains(node)) 
            || node == startNode 
            || node == targetNode;


        private void EvaluateNode(NavNode node)
        {
            int moveToNodeCost = currentNode.gCost + 
                                 GetDistanceCost(currentNode, node) +
                                 evaluator.GetDifficulty(node);
            bool isInOpenSet = openSet.Contains(node);
            if (moveToNodeCost < node.gCost || !isInOpenSet)
            {
                node.gCost = moveToNodeCost;
                node.hCost = GetDistanceCost(node, targetNode);
                node.parent = currentNode;

                if (!isInOpenSet)
                    openSet.Add(node);
            }
        }

        private int GetDistanceCost(NavNode a, NavNode b)
        {
            int xDistance = Mathf.Abs(a.X - b.X);
            int yDistance = Mathf.Abs(a.Y - b.Y);

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
            while (currentNode != startNode)
            {
                path.Add(currentNode.position);
                currentNode = currentNode.parent;
                length++;
                if (length > MAX_LENGTH)
                    return false;
            }
            path.Reverse();
            return true;
        }

        public Vector2Int GetTargetPosition()
        { 
            if (isValid && path.Count > 0) 
                return path[path.Count - 1];
            return default;
        }

        public bool TryToGetIndexOfPosition(Vector2Int position, out int index)
        {
            index = -1;
            for (int i = 0; i < path.Count; i++)
            {
                Vector2Int checkPosition = path[i];
                if (checkPosition == position)
                {
                    index = i;
                    return true;
                }
            }
            return false;
        }
    }
}