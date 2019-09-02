using UnityEngine;
using HexGen;
using System.Collections.Generic;
using System.Linq;

namespace LineDrawingExample
{
    public class LineDrawing : MonoBehaviour
    {
        private enum MouseClick
        {
            Left, Right
        }

        public GameObject Selection;

        private List<GameObject> line = new List<GameObject>();

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
                    Hex hex = HexInfo.PixelToHex(hit.point, hit.transform.GetComponent<Generator>().HexGrid.Hexes);

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
                        List<Hex> hexes = GetComponent<HexLine>().GetHexLine(startNode, endNode);
                        Vector3[] positions = hexes.Select(x => x.WorldPos).ToArray();

                        for (int i = line.Count - 1; i >= 0; --i)
                        {
                            Destroy(line[i]);
                        }
                        line.Clear();

                        LineRenderer lineRenderer = GetComponent<LineRenderer>();
                        lineRenderer.positionCount = positions.Count();

                        for (int i = 0; i < hexes.Count; i++)
                        {
                            positions[i].y += 1f; // To avoid clipping
                            line.Add(Instantiate(Selection, hexes[i].WorldPos, Quaternion.identity));
                        }

                        lineRenderer.SetPositions(positions);
                    }
                }
            }
        }
    }
}
