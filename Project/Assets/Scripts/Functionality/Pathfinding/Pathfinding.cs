using System.Collections.Generic;
using UnityEngine;

namespace HexGen
{
    public abstract class Pathfinding : ScriptableObject
    {
        public abstract List<Hex> Search(Hex startNode, Hex endNode);
    }
}
