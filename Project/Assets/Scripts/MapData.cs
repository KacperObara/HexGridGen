using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace HexGen
{
    [CreateAssetMenu(fileName = "MapData")]
    public class MapData : ScriptableObject
    {
        [HideInInspector]
        public float[,] HeightMap;

        public void ChangeHexTerrainType(int index, TerrainType terrainType)
        {
            Hexes[index].TerrainType = terrainType;
        }

        public int[] GetAllTerrainIndexes()
        {
            return Hexes.Select(x => x.TerrainType.TextureIndex).ToArray();
        }

        public void LoadTerrainData(MapSettings mapSettings, int[] textureIndexes)
        {
            for (int i = 0; i < Hexes.Length; ++i)
            {
                Hexes[i].TerrainType = mapSettings.TerrainTypes[textureIndexes[i]];
            }
        }

        [HideInInspector]
        public Hex[] Hexes;
    }
}
