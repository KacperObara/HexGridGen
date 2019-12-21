using UnityEngine;
using ExtensionMethods;
using UnityEditor;

namespace HexGen
{
    [CreateAssetMenu]
    public class DefaultGridGenerator : GridGenerator
    {
        private static readonly string ASSET_PATH = "Assets/obj.asset";

        private MapData mapData;
        private MapSettings settings;

        private void Validate()
        {
            if (settings.WorldHeight <= 0)
            {
                settings.WorldHeight = 1;
                Debug.LogWarning("World Height can't be < 1. Changing to 1");
            }

            if (settings.WorldWidth <= 0)
            {
                settings.WorldWidth = 1;
                Debug.LogWarning("World Width can't be < 1. Changing to 1");
            }
        }

        public override void Initialize(Generator generator)
        {
            this.mapData = generator.MapData;
            this.settings = generator.MapSettings;
        }

        public override void Generate()
        {
            Validate();
            CreateGrid();
        }

        public override void Clear()
        {
            AssetDatabase.DeleteAsset(ASSET_PATH);
        }

        private void CreateGrid()
        {
            mapData.Hexes = new Hex[settings.WorldWidth * settings.WorldHeight];
            CreateHexes();
        }

        private void CreateHexes()
        {
            Clear();

            AssetDatabase.CreateAsset(CreateInstance<EmptyAsset>(), ASSET_PATH);

            int i = 0;
            for (int z = 0; z < settings.WorldHeight; ++z)
            {
                for (int x = 0; x < settings.WorldWidth; ++x)
                {
                    CreateHex(x, z, i++);
                }
            }
        }

        private void CreateHex(int x, int z, int i)
        {
            // https://answers.unity.com/questions/644316/custom-inspector-scriptableobject-list-shows-type.html
            // It's solution to the problem with "serialization depth limit 7 exceeded"
            // I changed Hex script to inherit from ScriptableObject. With lines below, I can skip creating asset for every hex

            mapData.Hexes[i] = CreateInstance<Hex>();

            AssetDatabase.AddObjectToAsset(mapData.Hexes[i], ASSET_PATH);

            mapData.Hexes[i].hideFlags = HideFlags.HideInHierarchy;

            mapData.Hexes[i].Initialize(new Vector2Int(x, z), CalcHexWorldPos(x, z));

            PickNeighbors(x, z, i);
        }

        private void PickNeighbors(int x, int z, int i)
        {
            // Modifing this if can result in (going around the world) behaviour
            if (x > 0)
            {
                mapData.Hexes[i].SetNeighbor(HexDirection.W, mapData.Hexes[i - 1]);
            }
            if (z > 0)
            {
                if ((z & 1) == 0)
                {
                    mapData.Hexes[i].SetNeighbor(HexDirection.SE, mapData.Hexes[i - settings.WorldWidth]);
                    if (x > 0)
                    {
                        mapData.Hexes[i].SetNeighbor(HexDirection.SW, mapData.Hexes[i - settings.WorldWidth - 1]);
                    }
                }
                else
                {
                    mapData.Hexes[i].SetNeighbor(HexDirection.SW, mapData.Hexes[i - settings.WorldWidth]);
                    if (x < settings.WorldWidth - 1)
                    {
                        mapData.Hexes[i].SetNeighbor(HexDirection.SE, mapData.Hexes[i - settings.WorldWidth + 1]);
                    }
                }
            }
        }

        private Vector3 CalcHexWorldPos(int x, int z)
        {
            Vector3 pos = Vector3.zero;
            pos.x = x * HexData.InnerRadius * 2;
            pos.z = z * HexData.InnerRadius * Mathf.Sqrt(3);

            if (z.IsEven() == false)
                pos.x += HexData.InnerRadius;

            //pos.x *= grid.offsetMultiplier;
            //pos.z *= grid.offsetMultiplier;

            return pos;
        }
    }
}


