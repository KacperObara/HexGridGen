using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GridInfo : ScriptableObject
{
    public int WorldWidth;
    public int WorldHeight;
    public float offsetMultiplier;

    public Hex[] Hexes;
}
