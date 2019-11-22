using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HexGen;
using System.Linq;

namespace HexGenExampleGame1
{
    [RequireComponent(typeof(LineRenderer))]
    public class ShootingManager : MonoBehaviour
    {
        private BoardManager boardManager;
        private LineRenderer lineRenderer;
        private List<Hex> hexLine = new List<Hex>();

        private bool shooting = false;
        private int interval = 3;

        void Awake()
        {
            boardManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<BoardManager>();
            lineRenderer = GetComponent<LineRenderer>();
        }

        void FixedUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                shooting = !shooting;
            }

            if (shooting == true)
            {
                if (Time.frameCount % interval == 0)
                {
                    HandleInput();
                }
            }
        }

        void HandleInput()
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.GetComponent<Generator>())
                {
                    Hex hitHex = HexData.PixelToHex(hit.point, hit.transform.GetComponent<Generator>().MapData.Hexes);
                    
                    hexLine = GetComponent<HexLine>().GetHexLine(boardManager.SelectedObject.GetComponent<Unit>().OccupiedHex, hitHex);

                    lineRenderer.positionCount = hexLine.Count;

                    Vector3[] positions = hexLine.Select(x => x.WorldPos).ToArray();

                    for (int i = 0; i < hexLine.Count; i++)
                    {
                        positions[i].y += 1f; // To avoid clipping
                    }

                    lineRenderer.SetPositions(positions);
                }
            }
        }
    }

}