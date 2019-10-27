using System.Collections.Generic;
using Unity.Jobs;
using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace HexGen
{
    [CreateAssetMenu(fileName = "AStarPathfindingWithJobs", menuName = "Pathfinding/A*Jobs")]
    public class AStarPathfindingJobs : Pathfinding
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
                for (int i = 0; i < HexData.HexSides; ++i)
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

                    int childG = HexData.GetDistance(children[i].Node, startNode, true);

                    if (FindInList(openList, children[i]))
                        if (childG > HexData.GetDistance(currentNode.Node, startNode, true))
                            continue;

                    openList.Add(children[i]);
                }
            }

            return new List<Hex>();
        }

        private HexNode GetLowestDist(List<HexNode> openList, Hex startNode, Hex endNode)
        {
            NativeArray<int3> coords = new NativeArray<int3>(openList.Count, Allocator.TempJob);
            NativeArray<int> coordsMovementCost = new NativeArray<int>(openList.Count, Allocator.TempJob);
            NativeArray<int> results = new NativeArray<int>(openList.Count, Allocator.TempJob);

            for (int i = 0; i < openList.Count; ++i)
            {
                coords[i] = new int3(openList[i].Node.CubeLocalPos.x, 
                                     openList[i].Node.CubeLocalPos.y, 
                                     openList[i].Node.CubeLocalPos.z);
                coordsMovementCost[i] = openList[i].Node.TerrainType.MovementCost;
            }

            GetLowestDistanceJob parrarelJob = new GetLowestDistanceJob
            {
                coords = coords,
                coordsMovementCost = coordsMovementCost,
                results = results,
                startNode = new int3(startNode.CubeLocalPos.x,
                                     startNode.CubeLocalPos.y,
                                     startNode.CubeLocalPos.z),
                endNode = new int3(endNode.CubeLocalPos.x,
                                   endNode.CubeLocalPos.y,
                                   endNode.CubeLocalPos.z),
            };

            JobHandle jobHandle = parrarelJob.Schedule(openList.Count, openList.Count / 4);
            jobHandle.Complete();

            int index = -1;
            int lowestF = int.MaxValue;
            for (int i = 0; i < results.Length; ++i)
            {
                if (results[i] < lowestF)
                {
                    lowestF = results[i];
                    index = i;
                }
            }

            coords.Dispose();
            coordsMovementCost.Dispose();
            results.Dispose();

            return openList[index];
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

    [BurstCompile]
    struct GetLowestDistanceJob : IJobParallelFor
    {
        [ReadOnly] public NativeArray<int3> coords;
        [ReadOnly] public NativeArray<int> coordsMovementCost;
        [WriteOnly] public NativeArray<int> results;
        [ReadOnly] public int3 startNode;
        [ReadOnly] public int3 endNode;

        public void Execute(int index)
        {
            // Distance between current node and start
            int G = (math.abs(coords[index].x - startNode.x) +
                     math.abs(coords[index].y - startNode.y) +
                     math.abs(coords[index].z - startNode.z) * coordsMovementCost[index]) / 2;
            // Distance between current node and end
            int H = (math.abs(coords[index].x - endNode.x) +
                     math.abs(coords[index].y - endNode.y) +
                     math.abs(coords[index].z - endNode.z) * coordsMovementCost[index]) / 2;
            // Total cost of the node
            results[index] = G + H;
        }
    }
}