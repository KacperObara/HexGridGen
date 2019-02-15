using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GridSettings : ScriptableObject
{
    public int WorldWidth;
    public int WorldHeight;

    public Hex hexPrefab;
}
