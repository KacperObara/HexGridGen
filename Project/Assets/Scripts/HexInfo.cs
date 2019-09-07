using System;
using UnityEngine;

namespace HexGen
{
    public class HexInfo
    {
        public const float OuterRadius = 10f;
        public const float InnerRadius = 8.6602540378f; // http://www.drking.org.uk/hexagons/misc/ratio.html

        public const int HexVerticesCount = 6;
        public const int HexSides = 6;

        public static readonly Vector3[] vertices = {
        new Vector3(0f, 0f, OuterRadius),
        new Vector3(InnerRadius, 0f, 0.5f * OuterRadius),
        new Vector3(InnerRadius, 0f, -0.5f * OuterRadius),
        new Vector3(0f, 0f, -OuterRadius),
        new Vector3(-InnerRadius, 0f, -0.5f * OuterRadius),
        new Vector3(-InnerRadius, 0f, 0.5f * OuterRadius),
        new Vector3(0f, 0f, OuterRadius)
        };

        public static AxialCoordinates LocalToAxial(int x, int y)
        {
            var q = x - (y - (y & 1)) / 2;
            var r = y;
            
            return new AxialCoordinates(q, r);
        }

        public static Vector2Int AxialToLocal(int q, int r)
        {
            var x = q + (r - (r & 1)) / 2;
            var y = r;

            return new Vector2Int(x, y);
        }

        public static AxialCoordinates CubeToAxial(CubeCoordinates cube)
        {
            int q = cube.x;
            int r = cube.z;
            return new AxialCoordinates(q, r);
        }

        public static AxialCoordinates CubeToAxial(int x, int y, int z)
        {
            int q = x;
            int r = z;
            return new AxialCoordinates(q, r);
        }

        public static AxialCoordinates CubeToAxial(int x, int z)
        {
            int q = x;
            int r = z;
            return new AxialCoordinates(q, r);
        }

        public static CubeCoordinates AxialToCube(AxialCoordinates axial)
        {
            int x = axial.q;
            int z = axial.r;
            int y = -x - z;
            return new CubeCoordinates(x, y, z);
        }

        public static CubeCoordinates AxialToCube(int q, int r)
        {
            int x = q;
            int z = r;
            int y = -x - z;
            return new CubeCoordinates(x, y, z);
        }

        public static AxialCoordinates AxialRound(float q, float r)
        {
            float x = q;
            float z = r;
            float y = -x - z;
            return CubeToAxial(CubeRound(x, y, z));
        }

        public static CubeCoordinates CubeRound(float x, float y, float z)
        {
            int rx = (int)Mathf.Round(x);
            int ry = (int)Mathf.Round(y);
            int rz = (int)Mathf.Round(z);

            var x_diff = Mathf.Abs(rx - x);
            var y_diff = Mathf.Abs(ry - y);
            var z_diff = Mathf.Abs(rz - z);

            if (x_diff > y_diff && x_diff > z_diff)
                rx = -ry - rz;
            else if (y_diff > z_diff)
                ry = -rx - rz;
            else
                rz = -rx - ry;

            return new CubeCoordinates(rx, ry, rz);
        }

        public static int GetDistance(Hex first, Hex second, bool withCost)
        {
            CubeCoordinates a = first.CubeLocalPos;
            CubeCoordinates b = second.CubeLocalPos;

            if (withCost)
                return (Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y) + Mathf.Abs(a.z - b.z) * first.TerrainType.MovementCost) / 2;
            else
                return (Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y) + Mathf.Abs(a.z - b.z)) / 2;
        }

        public static Hex CubeToHex(CubeCoordinates coords, Hex[] hexes)
        {
            return AxialToHex(CubeToAxial(coords), hexes);
        }

        public static Hex AxialToHex(AxialCoordinates axial, Hex[] hexes)
        {
            int index = Array.FindIndex(hexes, element => (element.AxialLocalPos.q == axial.q)
                                                       && (element.AxialLocalPos.r == axial.r));
            Debug.Assert(index >= 0);
            return hexes[index];
        }

        public static Hex PixelToHex(Vector3 mousePosition, Hex[] hexes)
        {
            float q = (Mathf.Sqrt(3f) / 3f * mousePosition.x - 1f / 3f * mousePosition.z) / OuterRadius;
            float r = (2f / 3f * mousePosition.z) / OuterRadius;
            AxialCoordinates axial = AxialRound(q, r);

            return AxialToHex(axial, hexes);
        }
    }
}
