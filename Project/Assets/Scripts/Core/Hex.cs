using ExtensionMethods;
using System.Collections.Generic;
using UnityEngine;

namespace HexGen
{
    public class Hex : ScriptableObject
    {
        public Vector2Int LocalPos { get; private set; }
        public AxialCoordinates AxialLocalPos
        {
            get { return HexData.LocalToAxial(LocalPos.x, LocalPos.y); }
            private set { }
        }

        public CubeCoordinates CubeLocalPos
        {
            get { return HexData.AxialToCube(HexData.LocalToAxial(LocalPos.x, LocalPos.y)); }
            private set { }
        }

        public Vector3 WorldPos { get; private set; }

        public void Initialize(Vector2Int localPos, Vector3 worldPosition)
        {
            this.LocalPos = localPos;
            this.WorldPos = worldPosition;
        }

        public TerrainType TerrainType { get; set; }

        private Hex[] neighbors = new Hex[6];

        public Hex GetNeighbor(HexDirection direction)
        {
            return neighbors[(int)direction];
        }

        public void SetNeighbor(HexDirection direction, Hex cell)
        {
            neighbors[(int)direction] = cell;
            cell.neighbors[(int)direction.GetOpposite()] = this;
        }

        public static float GetDistance(Hex firstHex, Hex secondHex)
        {
            return Vector3.Distance(firstHex.WorldPos, secondHex.WorldPos);
        }
    }
}