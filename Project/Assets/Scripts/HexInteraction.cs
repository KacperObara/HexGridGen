using HexGen;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexInteraction : MonoBehaviour
{
    public static Hex SelectHexagon(Vector3 mousePosition, GridSettings grid)
    {
        int x = Mathf.RoundToInt(mousePosition.x / (Hex.InnerRadius * 2));
        int z = Mathf.RoundToInt(mousePosition.z / (Hex.OuterRadius * 2 * 0.75f));

        Debug.Log("Touched: " + x + "  " + z);
        Debug.Log("Mouse: " + mousePosition.x + " " + mousePosition.y + " " + mousePosition.z);
        return null;
    }
}
