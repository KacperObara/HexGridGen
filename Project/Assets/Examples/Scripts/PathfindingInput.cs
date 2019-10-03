using UnityEngine;
using HexGen;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace PathfindingExample
{
    public class PathfindingInput : MonoBehaviour
    {
        private enum MouseClick
        {
            Left, Right
        }

        public GameObject Selection;

        GameObject start;
        GameObject end;

        Hex startNode;
        Hex endNode;

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                HandleInput(MouseClick.Left);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                HandleInput(MouseClick.Right);
            }
        }

        void HandleInput(MouseClick click)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.GetComponent<Generator>())
                {
                    Hex hex = HexData.PixelToHex(hit.point, hit.transform.GetComponent<Generator>().MapData.Hexes);

                    if (click == MouseClick.Left)
                    {
                        startNode = hex;
                    }
                    else if (click == MouseClick.Right)
                    {
                        endNode = hex;
                    }

                    if (startNode != null && endNode != null)
                    {
                        List<Hex> hexes = GetComponent<PathfindingWithJobsSystem>().Search(startNode, endNode);

                        Vector3[] positions = hexes.Select(x => x.WorldPos).ToArray();

                        Destroy(start);
                        Destroy(end);

                        start = Instantiate(Selection, startNode.WorldPos, Quaternion.identity);
                        end = Instantiate(Selection, endNode.WorldPos, Quaternion.identity);

                        LineRenderer lineRenderer = GetComponent<LineRenderer>();
                        lineRenderer.positionCount = positions.Count();

                        for (int i = 0; i < hexes.Count; i++)
                        {
                            positions[i].y += 1f; // To avoid clipping
                        }

                        lineRenderer.SetPositions(positions);
                    }
                }
            }
        }
    }
}
