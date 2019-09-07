using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexGen
{
    [CreateAssetMenu(fileName = "MapSettings")]
    public class MapSettings : ScriptableObject
    {
        public int WorldWidth;
        public int WorldHeight;

        public float Scale;
        public int Octaves;
        [Range(0, 1)]
        public float Persistance;
        public float Lacunarity;

        [Tooltip("If 0, then generate seed")]
        public int Seed;

        [HideInInspector]
        public int ActualSeed;

        public TerrainType[] TerrainTypes;

        public void Load(MapSettings settings)
        {
            this.WorldWidth = settings.WorldWidth;
            this.WorldHeight = settings.WorldHeight;
            this.Scale = settings.Scale;
            this.Octaves = settings.Octaves;
            this.Persistance = settings.Persistance;
            this.Lacunarity = settings.Lacunarity;
            this.Seed = settings.ActualSeed;

            this.TerrainTypes = settings.TerrainTypes;
        }
    }
}
