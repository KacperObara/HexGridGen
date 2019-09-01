using UnityEngine;
using ExtensionMethods;

namespace HexGen
{
    public class GridGenerator : MonoBehaviour
    {
        public HexGrid Grid;

        private HexMeshGen meshGen;

        public void Validate()
        {
            if (Grid.WorldHeight <= 0)
                Grid.WorldHeight = 1;

            if (Grid.WorldWidth <= 0)
                Grid.WorldWidth = 1;
        }

        void Start()
        {
            if (Grid.Hexes == null)
            {
                Validate();
                CreateGrid();
                //GameObject.FindGameObjectWithTag("Grid").GetComponent<PerlinNoise>().ApplyColors();
            }
        }

        public void StartGen()
        {
            Validate();

            CreateGrid();

            meshGen = GetComponent<HexMeshGen>();
            meshGen.Initialize();
            meshGen.Triangulate(Grid.Hexes);
        }

        private void CreateGrid()
        {
            Grid.Hexes = new Hex[Grid.WorldWidth * Grid.WorldHeight];

            int i = 0;
            for (int z = 0; z < Grid.WorldHeight; ++z)
            {
                for (int x = 0; x < Grid.WorldWidth; ++x)
                {
                    CreateHex(x, z, i++);
                }
            }
        }

        private void CreateHex(int x, int z, int i)
        {
            Grid.Hexes[i] = new Hex(new Vector2Int(x, z), CalcHexPos(x, z));

            if (i > 0)
            {
                Grid.Hexes[i].SetNeighbor(HexDirection.W, Grid.Hexes[i - 1]);
            }
            if (z > 0)
            {
                if ((z & 1) == 0)
                {
                    Grid.Hexes[i].SetNeighbor(HexDirection.SE, Grid.Hexes[i - Grid.WorldWidth]);
                    if (x > 0)
                    {
                        Grid.Hexes[i].SetNeighbor(HexDirection.SW, Grid.Hexes[i - Grid.WorldWidth - 1]);
                    }
                }
                else
                {
                    Grid.Hexes[i].SetNeighbor(HexDirection.SW, Grid.Hexes[i - Grid.WorldWidth]);
                    if (x < Grid.WorldWidth - 1)
                    {
                        Grid.Hexes[i].SetNeighbor(HexDirection.SE, Grid.Hexes[i - Grid.WorldWidth + 1]);
                    }
                }
            }
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

