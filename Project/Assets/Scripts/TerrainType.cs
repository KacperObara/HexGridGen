using UnityEngine;

namespace HexGen
{
    [System.Serializable]
    public class TerrainType
    {
        public int TextureIndex;
        public string Name;
        public float NoiseHeight;

        public int MovementCost;

        public bool Passable;
    }
}

