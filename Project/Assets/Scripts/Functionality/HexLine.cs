using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexGen
{
    public class HexLine : MonoBehaviour
    {
        public MapData MapData;

        private float Lerp(float first, float second, float time)
        {
            return (first + (second - first) * time);
        }

        private Vector3 CubeLerp(Hex first, Hex second, float time)
        {
            return new Vector3(Lerp(first.WorldPos.x, second.WorldPos.x, time),
                               Lerp(first.WorldPos.y, second.WorldPos.y, time),
                               Lerp(first.WorldPos.z, second.WorldPos.z, time));
        }

        public List<Hex> GetHexLine(Hex start, Hex end)
        {
            List<Hex> hexes = new List<Hex>();

            int length = HexData.GetDistance(start, end, false);

            for (int i = 0; i < length; i++)
            {
                Vector3 coords = CubeLerp(start, end, 1.0f / length * i);
                hexes.Add(HexData.PixelToHex(coords, MapData.Hexes));
            }

            if (!hexes.Contains(end))
                hexes.Add(end);

            return hexes;
        }
    }
}
