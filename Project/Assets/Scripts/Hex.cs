using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex
{
    private Vector2Int localPosition;
    public Vector3 WorldPosition { get; private set; }

    public Hex(Vector2Int localPosition, Vector3 worldPosition)
    {
        this.localPosition = localPosition;
        this.WorldPosition = worldPosition;
    }
}
