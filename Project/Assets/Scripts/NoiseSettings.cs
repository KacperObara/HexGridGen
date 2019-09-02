using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexGen
{
    [CreateAssetMenu(fileName = "NoiseSettings")]
    public class NoiseSettings : ScriptableObject
    {
        public float Scale;

        public int Octaves;

        [Range(0, 1)]
        public float Persistance;
        public float Lacunarity;

        [Tooltip("If 0, then generate seed")]
        public int Seed;

        //public List<Material> Materials;

        public TerrainType[] TerrainTypes;
    }
}
