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

        public static AxialCoordinates OffsetToAxial(int x, int z)
        {
            var q = x - (z - (z & 1)) / 2;
            var r = z;

            return new AxialCoordinates(q, r);
        }

        public static Vector2Int AxialToOffset(int ax, int az)
        {
            var x = ax + (az - (az & 1)) / 2;
            var z = az;

            return new Vector2Int(x, z);
        }

        public static AxialCoordinates CubeToAxial(CubeCoordinates cube)
        {
            int q = cube.x;
            int r = cube.z;
            return new AxialCoordinates(q, r);
        }

        public static CubeCoordinates AxialToCube(AxialCoordinates axial)
        {
            int x = axial.q;
            int y = axial.r;
            int z = -x - y;
            return new CubeCoordinates(x, y, z);
        }

        public static AxialCoordinates RoundPixelToHex(float q, float r)
        {
            float x = q;
            float y = r;
            float z = -x - y;
            return CubeToAxial(RoundPixelToHex(x, y, z));
        }

        public static CubeCoordinates RoundPixelToHex(float x, float y, float z)
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
    }
}
