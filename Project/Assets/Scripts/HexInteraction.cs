using HexGen;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexGen
{
    public class HexInteraction : MonoBehaviour
    {
        public static Hex SelectHexagon(Vector3 mousePosition, HexGrid grid)
        {
            float q = (Mathf.Sqrt(3f) / 3f * mousePosition.x - 1f / 3f * mousePosition.z) / HexInfo.OuterRadius;
            float r = (2f / 3f * mousePosition.z) / HexInfo.OuterRadius;
            AxialCoordinates a = HexInfo.RoundPixelToHex(q, -q - r);

            ///
            Hex h = grid.GetHex(a.q, a.r);
            Debug.Log("Pozycja obliczona: Lokalnie:  " + h.LocalPos.x + " " + h.LocalPos.y);
            Debug.Log("Pozycja obliczona: Axial:     " + h.AxialLocalPos.q + " " + h.AxialLocalPos.r);
            Debug.Log("Pozycja obliczona: Cube:      " + h.CubeLocalPos.x + " " + h.CubeLocalPos.y + " " + h.CubeLocalPos.z);
            Debug.Log("Pozycja obliczona: World pos: " + h.WorldPos.x + " " + h.WorldPos.y + " " + h.WorldPos.z);
            //AxialCoordinates b = grid.GetHex(a.q, a.r).AxialLocalPos;
            ///
            Debug.Log(h.GetNeighbor(HexDirection.W).LocalPos);


            return grid.GetHex(a.q, a.r);
        }
    }
}