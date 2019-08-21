using ExtensionMethods;
using HexGen;
using UnityEngine;

public class Hex
{
    public AxialCoordinates LocalPosition { get; private set; }
    public Vector3 WorldPosition { get; private set; }

    public Vector2Int TMPNormal;

    public Hex(AxialCoordinates localPosition, Vector3 worldPosition, Vector2Int TMPNormal)
    {
        this.LocalPosition = localPosition;
        this.WorldPosition = worldPosition;
        this.TMPNormal = TMPNormal;
    }

    //int Elevation { get; set; }

    Hex[] neighbors = new Hex[6];

    public Color color;

    public Hex GetNeighbor(HexDirection direction)
    {
        return neighbors[(int)direction];
    }

    public void SetNeighbor(HexDirection direction, Hex cell)
    {
        neighbors[(int)direction] = cell;
        cell.neighbors[(int)direction.GetOpposite()] = this;
    }
}