using UnityEngine;

namespace HexGen
{
    [System.Serializable]
    public class TerrainType
    {
        public string Name;
        public float NoiseHeight;

        public int MovementCost;

        public bool Passable;
    }
}

