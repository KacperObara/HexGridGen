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
    }
}
