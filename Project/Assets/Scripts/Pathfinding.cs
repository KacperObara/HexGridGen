using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexGen
{
    public class Pathfinding : MonoBehaviour
    {
        public GameObject Prefab;

        public void Search(Hex startNode, Hex endNode)
        {
            Instantiate(Prefab, new Vector3(startNode.WorldPos.x, startNode.WorldPos.y + 5, startNode.WorldPos.z), Quaternion.identity);
            Instantiate(Prefab, new Vector3(endNode.WorldPos.x, endNode.WorldPos.y + 5, endNode.WorldPos.z), Quaternion.identity);

            List<Hex> openList = new List<Hex>();
            List<Hex> closedList = new List<Hex>();

            openList.Add(startNode);

            int index = 0;
            while (openList.Count > 0)
            {
                Hex currentNode = GetLowestDist(openList, startNode, endNode);
                Debug.Log("Wal sie");

                openList.Remove(currentNode);
                closedList.Add(currentNode);

                if (currentNode == endNode)
                {
                    for (int i = 0; i < closedList.Count; i++)
                    {
                        //Debug.Log(closedList[i].LocalPos);
                        Instantiate(Prefab, closedList[i].WorldPos, Quaternion.identity);
                    }
                    return;
                }

                List<Hex> children = new List<Hex>();
                for (int i = 0; i < HexInfo.HexSides; ++i)
                {
                    if (currentNode.GetNeighbor((HexDirection)i) != null)
                    { // walkable terrain here or by childH?
                      // Use the distance formula, scaled to match the 
                      // movement costs. For example if your movement cost 
                      // is 5 per hex, then multiply the distance by 5.
                        children.Add(currentNode.GetNeighbor((HexDirection)i));
                    }
                }

                for (int i = 0; i < children.Count; ++i)
                {
                    if (closedList.Contains(children[i]))
                        continue;

                    int childG = GetDistance(children[i], startNode);
                    int childH = GetDistance(children[i], endNode);
                    int childF = childG + childH;

                    if (openList.Contains(children[i]))
                        if (childG > GetDistance(currentNode, startNode))
                            continue;

                    openList.Add(children[i]);
                }

            }
        }

        int GetDistance(Hex currentNode, Hex targetNode)
        {
            return Mathf.Abs(targetNode.LocalPos.x + currentNode.LocalPos.x)
                 + Mathf.Abs(targetNode.LocalPos.y - currentNode.LocalPos.y);
        }

        Hex GetLowestDist(List<Hex> openList, Hex startNode, Hex endNode)
        {
            Hex currentNode = null;
            int lowestF = int.MaxValue;

            for (int i = 0; i < openList.Count; ++i)
            {
                int G = GetDistance(openList[i], startNode);
                int H = GetDistance(openList[i], endNode);
                int F = G + H;

                if (F < lowestF)
                {
                    lowestF = F;
                    currentNode = openList[i];
                }
            }

            return currentNode;
        }
    }

    struct HexNode
    {
        public Hex Node;
        public Hex Parent;
    }
}


