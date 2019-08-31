using System;
using UnityEngine;

namespace HexGen
{
    public class HexInteraction
    {
        public static Hex SelectHexagon(Vector3 mousePosition, Hex[] hexes)
        {
            float q = (Mathf.Sqrt(3f) / 3f * mousePosition.x - 1f / 3f * mousePosition.z) / HexInfo.OuterRadius;
            float r = (2f / 3f * mousePosition.z) / HexInfo.OuterRadius;
            AxialCoordinates axial = HexInfo.PixelToAxial(q, -q - r);

            return AxialToHex(axial, hexes);
        }

        public static int GetDistance(Hex first, Hex second, bool withCost)
        {
            CubeCoordinates a = first.CubeLocalPos;
            CubeCoordinates b = second.CubeLocalPos;

            if (withCost)
                return (Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y) + Mathf.Abs(a.z - b.z) * first.TerrainType.movementCost) / 2;
            else
                return (Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y) + Mathf.Abs(a.z - b.z)) / 2;
        }

        public static Hex CubeToHex(CubeCoordinates coords, Hex[] hexes)
        {
            return AxialToHex(HexInfo.CubeToAxial(coords), hexes);
        }

        public static Hex AxialToHex(AxialCoordinates axial, Hex[] hexes)
        {
            int index = Array.FindIndex(hexes, element => (element.AxialLocalPos.q == axial.q)
                                                       && (element.AxialLocalPos.r == axial.r));
            Debug.Assert(index >= 0);
            return hexes[index];
        }
    }
}