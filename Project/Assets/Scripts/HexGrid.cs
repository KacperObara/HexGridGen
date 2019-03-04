using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexGen
{
    public class HexGrid : MonoBehaviour
    {
        public GridSettings settings;

        [SerializeField]
        private GridInfo grid;

        private MeshGen meshGen;

        private void Awake()
        {
            grid.Hexes = new Hex[settings.WorldWidth * settings.WorldHeight];
            GenerateGrid();
        }

        private void Start()
        {
            meshGen = GetComponent<MeshGen>();
            meshGen.Triangulate(grid.Hexes);
        }

        private void GenerateGrid()
        {
            int i = 0;
            for(int x = 0; x < settings.WorldWidth; ++x)
            {
                for (int z = 0; z < settings.WorldHeight; ++z)
                {
                    InstantiateHex(x, z, i++);
                }
            }
        }

        private void InstantiateHex(int x, int z, int i)
        {
            grid.Hexes[i] = Instantiate(settings.hexPrefab, CalcPos(x, z), Quaternion.identity);
            grid.Hexes[i].transform.parent = transform;
        }

        private Vector3 CalcPos(int x, int z)
        {
            Vector3 pos = Vector3.zero;
            pos.x = x * Hex.InnerRadius * 2;
            pos.z = z * Hex.InnerRadius * Mathf.Sqrt(3);

            if (z % 2 != 0)
                pos.x += Hex.InnerRadius;

            pos.x *= settings.offsetMultiplier;
            pos.z *= settings.offsetMultiplier;

            return pos;
        }
    }
}

