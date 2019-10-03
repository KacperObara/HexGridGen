using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexGen
{
    [CreateAssetMenu(fileName = "MapData")]
    public class MapData : ScriptableObject
    {
        [HideInInspector]
        public float[,] HeightMap;

        [HideInInspector]
        public int[] TextureIndex;

        [HideInInspector]
        public Hex[] Hexes;

        //public void Load(SaveFile saveFile)
        //{
        //    this.TextureIndex = saveFile.EditedMapTextureData;
        //}
    }
}
