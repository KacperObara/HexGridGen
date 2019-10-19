using System.Collections.Generic;
using UnityEngine;
using HexGen;
using System.Linq;

public class Range : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="startNode">Node at which IMovable starts moving</param>
    /// <param name="IMovable">Object that can move through the hexes</param>
    /// <returns></returns>
    public List<Hex> GetHexesInRange(Hex startNode, IMovable IMovable)
    {
        List<HexNode> openList = new List<HexNode>();
        List<HexNode> closedList = new List<HexNode>();
        HashSet<Hex> resultList = new HashSet<Hex>();

        HexNode currentNode = new HexNode(startNode);
        openList.Add(currentNode);
        while (openList.Count > 0)
        {
            currentNode = openList[0];
            openList.Remove(currentNode);
            closedList.Add(currentNode);

            List<HexNode> children = new List<HexNode>();
            for (int i = 0; i < HexData.HexSides; ++i)
            {
                Hex childNode = currentNode.Node.GetNeighbor((HexDirection)i);
                if (childNode != null)
                {
                    if (childNode.TerrainType.Passable == true)
                    {
                        HexNode child = new HexNode(childNode, currentNode, currentNode.overallMovementCost + childNode.TerrainType.MovementCost);
                        children.Add(child);
                    }
                }
            }
            
            for (int i = 0; i < children.Count; ++i)
            {
                if (!closedList.Contains(children[i])) // to do zmiany
                {
                    if (children[i].overallMovementCost <= IMovable.Range)
                    {
                        openList.Add(children[i]);
                        resultList.Add(children[i].Node);
                    }
                }
            }
        }

        return resultList.ToList();
    }


    private class HexNode
    {
        public Hex Node;
        public HexNode Parent;
        public int overallMovementCost;

        public HexNode(Hex Node)
        {
            this.Node = Node;
        }

        public HexNode(Hex Node, HexNode Parent, int overallMovementCost)
        {
            this.Node = Node;
            this.Parent = Parent;
            this.overallMovementCost = overallMovementCost;
        }
    }
}
