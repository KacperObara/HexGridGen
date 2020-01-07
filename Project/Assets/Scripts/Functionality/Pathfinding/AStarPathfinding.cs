using System.Collections.Generic;
using UnityEngine;

namespace HexGen
{
    [CreateAssetMenu(fileName = "AStarPathfinding", menuName = "Pathfinding/A*")]
    public class AStarPathfinding : Pathfinding
    {
        /// <summary>
        /// Returns Path from startNode to endNode (including both)
        /// </summary>
        public override List<Hex> Search(Hex startNode, Hex endNode)
        {
            if (startNode.TerrainType.Passable == false || endNode.TerrainType.Passable == false || startNode == endNode)
                return new List<Hex>();

            List<HexNode> openList = new List<HexNode>();
            List<HexNode> closedList = new List<HexNode>();

            openList.Add(new HexNode(startNode));

            // Loop until you have no more nodes in graph
            while (openList.Count > 0)
            {
                HexNode currentNode = GetLowestDist(openList, startNode, endNode);

                openList.Remove(currentNode);
                closedList.Add(currentNode);

                // Found the goal
                if (currentNode.Node == endNode)
                {
                    List<Hex> path = new List<Hex>();
                    HexNode current = currentNode;
                    while (current.Node != startNode && current.Node != null)
                    {
                        path.Add(current.Node);
                        current = current.Parent;
                    }
                    path.Add(startNode);
                    path.Reverse();
                    return path;
                }

                List<HexNode> children = GenerateChildren(currentNode);
                
                // Loop through children
                for (int i = 0; i < children.Count; ++i)
                {
                    if (FindInList(closedList, children[i]))
                        continue;

                    int childG = HexData.GetDistance(children[i].Node, startNode, true);

                    if (FindInList(openList, children[i]))
                        if (childG > HexData.GetDistance(currentNode.Node, startNode, true))
                            continue;

                    openList.Add(children[i]);
                }
            }

            return new List<Hex>();
        }

        private List<HexNode> GenerateChildren(HexNode currentNode)
        {
            List<HexNode> children = new List<HexNode>();

            for (int i = 0; i < HexData.HexSides; ++i)
            {
                Hex child = currentNode.Node.GetNeighbor((HexDirection)i);

                if (child != null)
                    if (child.TerrainType.Passable == true)
                        children.Add(new HexNode(child, currentNode));
            }

            return children;
        }

        private HexNode GetLowestDist(List<HexNode> openList, Hex startNode, Hex endNode)
        {
            HexNode currentNode = null;
            int lowestF = int.MaxValue;

            for (int i = 0; i < openList.Count; ++i)
            {
                // Distance between current node and start
                int G = HexData.GetDistance(openList[i].Node, startNode, true);
                // Distance between current node and end
                int H = HexData.GetDistance(openList[i].Node, endNode, true);
                // Total cost of the node
                int F = G + H;

                if (F < lowestF)
                {
                    lowestF = F;
                    currentNode = openList[i];
                }
            }

            return currentNode;
        }

        private bool FindInList(List<HexNode> list, HexNode hexNode)
        {
            foreach (HexNode item in list)
            {
                if (hexNode.Node == item.Node)
                    return true;
            }
            return false;
        }


        private class HexNode
        {
            public Hex Node;
            public HexNode Parent;

            public HexNode(Hex Node)
            {
                this.Node = Node;
            }
            public HexNode(Hex Node, HexNode Parent)
            {
                this.Node = Node;
                this.Parent = Parent;
            }
        }
    }
}

/*
     Add it to "Found the goal" to debug the whole path that has been searched  
   
     List<Hex> debugSearchingPath = new List<Hex>();
     for (int i = 0; i < closedList.Count; i++)
     {
         debugSearchingPath.Add(closedList[i].Node);
     }

     return debugSearchingPath;
*/