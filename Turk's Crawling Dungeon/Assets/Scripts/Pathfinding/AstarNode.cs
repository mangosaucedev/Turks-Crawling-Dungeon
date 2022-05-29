using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Pathfinding
{
    public struct AstarNode : IEquatable<AstarNode>
    {
        public int x;
        public int y;
        public bool isPassable;
        public int difficulty;

        public Vector2Int Position => new Vector2Int(x, y);

        public AstarNode(int x, int y)
        {
            this.x = x;
            this.y = y;
            isPassable = false;
            difficulty = 0;
        }

        public bool Equals(AstarNode other) => 
            x == other.x 
            && y == other.y 
            && isPassable == other.isPassable 
            && difficulty == other.difficulty;
        
    }
}
