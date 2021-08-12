using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Pathfinding
{
    public interface INavEvaluator 
    {
        bool IsPassable(NavNode node);
    
        int GetDifficulty(NavNode node);
    }
}
