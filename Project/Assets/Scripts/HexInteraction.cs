using HexGen;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexInteraction : MonoBehaviour
{
    public static Hex SelectHexagon(Vector3 mousePosition, HexGrid grid)
    {
        float q = (Mathf.Sqrt(3f) / 3f * mousePosition.x - 1f / 3f * mousePosition.z) / HexInfo.OuterRadius;
        float r = (                                        2f / 3f * mousePosition.z) / HexInfo.OuterRadius;
        AxialCoordinates a = HexInfo.RoundPixelToHex(q, -q - r);
        Debug.Log("Pozycja obliczona: " + a.q + " " + a.r);
        AxialCoordinates b = grid.GetHex(a.q, a.r).LocalPosition;


        return grid.GetHex(a.q, a.r);
    }
}