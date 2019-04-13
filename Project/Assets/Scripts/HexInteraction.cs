using HexGen;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexInteraction : MonoBehaviour
{
    public static Hex SelectHexagon(Vector3 mousePosition, GridInfo grid)
    {
        float q = (Mathf.Sqrt(3f) / 3f * mousePosition.x - 1f / 3f * mousePosition.z) / HexInfo.OuterRadius;
        float r = (                                        2f / 3f * mousePosition.z) / HexInfo.OuterRadius;
        AxialCoordinates a = HexInfo.RoundPixelToHex(new AxialCoordinates(q, r));
        Debug.Log(a.q + " " + a.r);

        return null;
    }
}