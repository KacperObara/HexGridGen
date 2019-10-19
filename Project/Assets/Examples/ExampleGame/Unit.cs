using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HexGen;

public class Unit : MonoBehaviour, IMovable
{
    int range;
    Hex occupiedHex;

    public int Range { get => range; set => range = value; }
    public Hex OccupiedHex { get => occupiedHex; set => occupiedHex = value; }

    public void Initialize(Hex occupiedHex, int range)
    {
        this.OccupiedHex = occupiedHex;
        this.Range = range;
    }
}
