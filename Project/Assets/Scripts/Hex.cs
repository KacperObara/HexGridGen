using ExtensionMethods;
using HexGen;
using System;
using UnityEngine;

namespace HexGen
{
    public class Hex
    {
        public Vector2Int LocalPos { get; private set; }
        public AxialCoordinates AxialLocalPos
        {
            get { return HexInfo.OffsetToAxial(LocalPos.x, LocalPos.y); }
            private set { }
        }

        public CubeCoordinates CubeLocalPos
        {
            get { return HexInfo.AxialToCube(HexInfo.OffsetToAxial(LocalPos.x, LocalPos.y)); }
            private set { }
        }

        public Vector3 WorldPos { get; private set; }

        public Hex(Vector2Int localPos, Vector3 worldPosition)
        {
            this.LocalPos = localPos;
            this.WorldPos = worldPosition;
        }

        public TerrainType TerrainType;

        [SerializeField]
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
    }
}