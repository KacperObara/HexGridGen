using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    public GridSettings settings;

    private Hex[,] hexes;

    private void Awake()
    {
        hexes = new Hex[settings.WorldWidth, settings.WorldHeight];
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        for(int x = 0; x < settings.WorldWidth; ++x)
        {
            for (int z = 0; z < settings.WorldHeight; ++z)
            {
                InstantiateHex(x, z);
            }
        }
    }

    private void InstantiateHex(int x, int z)
    {
        //Vector3 pos = Vector3.zero;
        //pos.x = x * Hex.OuterRadius;
        //pos.z = z * Hex.OuterRadius;

        hexes[x, z] = Instantiate(settings.hexPrefab, CalcPos(x, z), Quaternion.identity);
        hexes[x, z].transform.parent = transform;
    }

    private Vector3 CalcPos(int x, int z)
    {
        Vector3 pos = Vector3.zero;
        pos.x = x * Hex.OuterRadius;
        pos.z = z * Hex.OuterRadius;
        pos.x = (x + z * 0.5f - z / 2) * (Hex.InnerRadius * 2f);
        return pos;
        /*
        float offset = 0f;

        if (z % 2 != 0)
            offset = Hex.InnerRadius / 2;

        return new Vector3(x * Hex.InnerRadius + offset, 0, z * Hex.OuterRadius);
        */
    }
}
