using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexGen
{
    [CreateAssetMenu(fileName = "NoiseSettings")]
    public class NoiseSettings : ScriptableObject
    {
        public float scale;

        public int octaves;
        [Range(0, 1)]

        public float persistance;
        public float lacunarity;

        //public List<Material> Materials;

        public TerrainType[] TerrainTypes;
    }
}
