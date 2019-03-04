using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex
{
    public const float OuterRadius = 10f;
    public const float InnerRadius = 8.6602540378f; // http://www.drking.org.uk/hexagons/misc/ratio.html

    public static readonly Vector3[] vertices = {
        new Vector3(0f, 0f, OuterRadius),
        new Vector3(InnerRadius, 0f, 0.5f * OuterRadius),
        new Vector3(InnerRadius, 0f, -0.5f * OuterRadius),
        new Vector3(0f, 0f, -OuterRadius),
        new Vector3(-InnerRadius, 0f, -0.5f * OuterRadius),
        new Vector3(-InnerRadius, 0f, 0.5f * OuterRadius),
        new Vector3(0f, 0f, OuterRadius)
    };


    private Vector2Int localPosition;
    public Vector3 WorldPosition { get; private set; }

    public Hex(Vector2Int localPosition, Vector3 worldPosition)
    {
        this.localPosition = localPosition;
        this.WorldPosition = worldPosition;
    }
}
