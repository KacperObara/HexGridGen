using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexGen
{
    public class HexLine : MonoBehaviour
    {
        public HexGrid grid;

        private float Lerp(float first, float second, float time)
        {
            return (first + (second - first) * time);
        }

        private Vector3 CubeLerp(Hex first, Hex second, float time)
        {
            return new Vector3(Lerp(first.CubeLocalPos.x, second.CubeLocalPos.x, time),
                               Lerp(first.CubeLocalPos.y, second.CubeLocalPos.y, time),
                               Lerp(first.CubeLocalPos.z, second.CubeLocalPos.z, time));
        }

        public List<Hex> GetHexLine(Hex start, Hex end)
        {
            List<Hex> hexes = new List<Hex>();

            int length = HexInteraction.GetDistance(start, end, false);

            for (int i = 0; i < length; i++)
            {
                Vector3 coords = CubeLerp(start, end, 1.0f / length * i);
                Debug.Log(coords);
                CubeCoordinates cube = HexInfo.PixelToCube(coords.x, coords.y, coords.z);
                Debug.Log(cube.x + " " + cube.y + " " + cube.z);
                //CubeCoordinates coords = CubeLerp(start, end, 1.0f / length * i);
                hexes.Add(HexInteraction.CubeToHex(cube, grid.Hexes));
                //hexes.Add(HexInteraction.CubeToHex(coords, grid.Hexes));
            }

            return hexes;
        }


        private void F()
        {

        }
    }
}
