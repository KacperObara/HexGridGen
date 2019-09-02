using UnityEngine;
using ExtensionMethods;

namespace HexGen
{
    [CreateAssetMenu]
    public class GridGenerator : GridGen
    {
        private HexGrid grid;

        private void Validate()
        {
            if (grid.WorldHeight <= 0)
            {
                grid.WorldHeight = 1;
                Debug.LogWarning("World Height can't be < 1. Changing to 1");
            }

            if (grid.WorldWidth <= 0)
            {
                grid.WorldWidth = 1;
                Debug.LogWarning("World Width can't be < 1. Changing to 1");
            }
        }

        //void Start()
        //{
        //    if (grid.Hexes == null)
        //    {
        //        Validate();
        //        CreateGrid();
        //    }
        //}

        public override void Initialize(Generator generator)
        {
            this.grid = generator.HexGrid;
        }

        public override void Generate()
        {
            Validate();
            CreateGrid();
        }

        private void CreateGrid()
        {
            grid.Hexes = new Hex[grid.WorldWidth * grid.WorldHeight];

            int i = 0;
            for (int z = 0; z < grid.WorldHeight; ++z)
            {
                for (int x = 0; x < grid.WorldWidth; ++x)
                {
                    CreateHex(x, z, i++);
                }
            }
        }

        private void CreateHex(int x, int z, int i)
        {
            grid.Hexes[i] = new Hex(new Vector2Int(x, z), CalcHexWorldPos(x, z));
            PickNeighbors(x, z, i);
        }

        private void PickNeighbors(int x, int z, int i)
        {
            if (i > 0)
            {
                grid.Hexes[i].SetNeighbor(HexDirection.W, grid.Hexes[i - 1]);
            }
            if (z > 0)
            {
                if ((z & 1) == 0)
                {
                    grid.Hexes[i].SetNeighbor(HexDirection.SE, grid.Hexes[i - grid.WorldWidth]);
                    if (x > 0)
                    {
                        grid.Hexes[i].SetNeighbor(HexDirection.SW, grid.Hexes[i - grid.WorldWidth - 1]);
                    }
                }
                else
                {
                    grid.Hexes[i].SetNeighbor(HexDirection.SW, grid.Hexes[i - grid.WorldWidth]);
                    if (x < grid.WorldWidth - 1)
                    {
                        grid.Hexes[i].SetNeighbor(HexDirection.SE, grid.Hexes[i - grid.WorldWidth + 1]);
                    }
                }
            }
        }

        private Vector3 CalcHexWorldPos(int x, int z)
        {
            Vector3 pos = Vector3.zero;
            pos.x = x * HexInfo.InnerRadius * 2;
            pos.z = z * HexInfo.InnerRadius * Mathf.Sqrt(3);

            if (z.IsEven() == false)
                pos.x += HexInfo.InnerRadius;

            pos.x *= grid.offsetMultiplier;
            pos.z *= grid.offsetMultiplier;

            return pos;
        }
    }
}

