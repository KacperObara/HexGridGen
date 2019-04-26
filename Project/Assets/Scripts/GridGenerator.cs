using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExtensionMethods;

namespace HexGen
{
    public class GridGenerator : MonoBehaviour
    {
        public HexGrid Grid;

        private HexMeshGen meshGen;

        private void Start()
        {
            Grid.Hexes = new Hex[Grid.WorldWidth * Grid.WorldHeight];
            GenerateGrid();

            meshGen = GetComponent<HexMeshGen>();
            meshGen.Triangulate(Grid.Hexes);
        }

        private void GenerateGrid()
        {
            int i = 0;
            for(int x = 0; x < Grid.WorldWidth; ++x)
            {
                for (int z = 0; z < Grid.WorldHeight; ++z)
                {
                    CreateHex(x, z, i++);
                }
            }
        }

        private void CreateHex(int x, int z, int i)
        {
            Grid.Hexes[i] = new Hex(HexInfo.OffsetToAxial(x, z), CalcHexPos(x, z));
        }

        private Vector3 CalcHexPos(int x, int z)
        {
            Vector3 pos = Vector3.zero;
            pos.x = x * HexInfo.InnerRadius * 2;
            pos.z = z * HexInfo.InnerRadius * Mathf.Sqrt(3);

            if (z.IsEven() == false)
                pos.x += HexInfo.InnerRadius;

            pos.x *= Grid.offsetMultiplier;
            pos.z *= Grid.offsetMultiplier;

            return pos;
        }
    }
}

