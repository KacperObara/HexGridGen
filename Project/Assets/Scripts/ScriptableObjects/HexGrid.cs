using System;
using UnityEngine;

[CreateAssetMenu]
public class HexGrid : ScriptableObject
{
    public int WorldWidth;
    public int WorldHeight;
    public float offsetMultiplier;

    public Hex[] Hexes;

    public Hex GetHex(int q, int r)
    {
        int index = Array.FindIndex(Hexes, element => (element.LocalPosition.q == q) 
                                                   && (element.LocalPosition.r == r));
        Debug.Assert(index >= 0);
        return Hexes[index];
    }
}
