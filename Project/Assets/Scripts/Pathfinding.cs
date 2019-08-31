using System.Collections.Generic;
using UnityEngine;

namespace HexGen
{
    public class Pathfinding : MonoBehaviour
    {
        /// <summary>
        /// Returns Path from startNode to endNode (including both)
        /// </summary>
        /// <param name="startNode"></param>
        /// <param name="endNode"></param>
        /// <returns></returns>
        public List<Hex> Search(Hex startNode, Hex endNode)
        {
            if (startNode.TerrainType.Passable == false || endNode.TerrainType.Passable == false || startNode == endNode)
                return new List<Hex>();

            List<HexNode> openList = new List<HexNode>();
            List<HexNode> closedList = new List<HexNode>();

            openList.Add(new HexNode(startNode));

            // Loop until you no more nodes in graph
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

                // Generate children
                List<HexNode> children = new List<HexNode>();
                for (int i = 0; i < HexInfo.HexSides; ++i)
                {
                    Hex child = currentNode.Node.GetNeighbor((HexDirection)i);

                    if (child != null)
                        if (child.TerrainType.Passable == true)
                            children.Add(new HexNode(child, currentNode));
                }

                // Loop through children
                for (int i = 0; i < children.Count; ++i)
                {
                    if (FindInList(closedList, children[i]))
                        continue;

                    int childG = HexInteraction.GetDistance(children[i].Node, startNode, true);

                    if (FindInList(openList, children[i]))
                        if (childG > HexInteraction.GetDistance(currentNode.Node, startNode, true))
                            continue;

                    openList.Add(children[i]);
                }
            }

            return new List<Hex>();
        }

        HexNode GetLowestDist(List<HexNode> openList, Hex startNode, Hex endNode)
        {
            HexNode currentNode = null;
            int lowestF = int.MaxValue;

            for (int i = 0; i < openList.Count; ++i)
            {
                // Distance between current node and start
                int G = HexInteraction.GetDistance(openList[i].Node, startNode, true);
                // Distance between current node and end
                int H = HexInteraction.GetDistance(openList[i].Node, endNode, true);
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


