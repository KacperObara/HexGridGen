using UnityEngine;

namespace HexGen
{
    [CreateAssetMenu (menuName = "Terrain Type")]
    public class TerrainType : ScriptableObject
    {
        public float NoiseHeight;
        public Color Color;

        public int movementCost;

        public bool Passable;
    }
}

