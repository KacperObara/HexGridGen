using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex
{
    public AxialCoordinates LocalPosition { get; private set; }
    public Vector3 WorldPosition { get; private set; }

    public Hex(AxialCoordinates localPosition, Vector3 worldPosition)
    {
        this.LocalPosition = localPosition;
        this.WorldPosition = worldPosition;
    }
}
