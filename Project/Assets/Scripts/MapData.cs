using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexGen
{
    [CreateAssetMenu(fileName = "MapData")]
    public class MapData : ScriptableObject
    {
        [HideInInspector]
        public float[,] HeightMap;
        [HideInInspector]
        public Color[,] ColorMap;

        [HideInInspector]
        public Hex[] Hexes;
    }
}
