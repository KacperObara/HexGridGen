using UnityEngine;

namespace HexGen
{
    public class HexInfo : MonoBehaviour
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

        /// <summary>
        /// Rounds pixel coordinates to according hex, for example clicking and selecting hex with mouse.
        /// </summary>
        /// <param name="axial"></param>
        /// <returns></returns>
        public static AxialCoordinates RoundPixelToHex(AxialCoordinates axial)
        {
            return CubeToAxial(CubeRound(AxialToCube(axial)));         /// For ease of implementation, method converts axial coordinates to cube, 
                                                                       /// then rounds values and converts back to axial.
        }

        public static AxialCoordinates CubeToAxial(CubeCoordinates cube)
        {
            var q = cube.x;
            var r = cube.z;
            return new AxialCoordinates(q, r);
        }

        public static CubeCoordinates AxialToCube(AxialCoordinates axial)
        {
            var x = axial.q;
            var y = axial.r;
            var z = -x - y;
            return new CubeCoordinates(x, y, z);
        }

        public static CubeCoordinates CubeRound(CubeCoordinates cube)
        {
            var rx = Mathf.Round(cube.x);
            var ry = Mathf.Round(cube.y);
            var rz = Mathf.Round(cube.z);

            var x_diff = Mathf.Abs(rx - cube.x);
            var y_diff = Mathf.Abs(ry - cube.y);
            var z_diff = Mathf.Abs(rz - cube.z);

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
