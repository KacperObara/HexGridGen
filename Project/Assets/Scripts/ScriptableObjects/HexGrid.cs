using System;
using UnityEngine;

namespace HexGen
{
    [CreateAssetMenu]
    public class HexGrid : ScriptableObject
    {
        public int WorldWidth;
        public int WorldHeight;
        public float offsetMultiplier;

        public float[,] HeightMap;
        public Color[,] ColorMap;

        [SerializeField]
        public Hex[] Hexes;

        public Hex GetHex(int q, int r)
        {
            int index = Array.FindIndex(Hexes, element => (element.AxialLocalPos.q == q)
                                                       && (element.AxialLocalPos.r == r));
            Debug.Assert(index >= 0);
            return Hexes[index];
        }
    }
}
