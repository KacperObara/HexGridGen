using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HexGen;

public class RangeMeasuring : MonoBehaviour
{
    MovableTest movable;
    public int MovementRange;
    public GameObject Selection;
    private List<GameObject> Selected = new List<GameObject>();

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleInput();
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
                Hex startHex = HexData.PixelToHex(hit.point, hit.transform.GetComponent<Generator>().MapData.Hexes);

                List<Hex> movementHexes = GetComponent<Range>().GetHexesInRange(startHex, new MovableTest(startHex, MovementRange));

                for (int i = Selected.Count - 1; i >= 0; --i)
                {
                    Destroy(Selected[i]);
                }
                Selected.Clear();

                for (int i = 0; i < movementHexes.Count; ++i)
                {
                    GameObject selectedTemp = Instantiate(Selection, movementHexes[i].WorldPos, Quaternion.identity);
                    Selected.Add(selectedTemp);
                }
            }
        }
    }


    private class MovableTest : IMovable
    {
        int range;
        Hex occupiedHex;

        public int Range { get => range; set => range = value; }
        public Hex OccupiedHex { get => occupiedHex; set => occupiedHex = value; }

        public MovableTest(Hex occupiedHex, int range)
        {
            this.OccupiedHex = occupiedHex;
            this.Range = range;
        }
    }
}