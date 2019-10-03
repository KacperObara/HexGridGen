using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexGen
{
    public class Generator : MonoBehaviour
    {
        [SerializeField]
        private GridGenerator gridGenerator;
        [SerializeField]
        private NoiseGenerator noiseGenerator;
        [SerializeField]
        public MeshGenerator meshGenerator;

        public MapData MapData;
        public MapSettings MapSettings;

        [Space]
        public SaveFile SaveFile;

        public void Generate()
        {
            gridGenerator.Initialize(this);
            noiseGenerator.Initialize(this);
            meshGenerator.Initialize(this);

            gridGenerator.Generate();
            noiseGenerator.Generate();
            meshGenerator.Generate();
        }

        public void LoadMap()
        {
            if (SaveFile == null)
            {
                Debug.LogWarning("There is no save file to load");
                return;
            }

            MapSettings.Load(SaveManager.LoadMapSettings(SaveFile));
            Generate();

            SaveManager.LoadEditedMapTextureData(SaveFile, ref MapData);
            Debug.Log(MapData.TextureIndex[0]);
            meshGenerator.Generate();
        }

        public void ClearMap()
        {
            gridGenerator.Clear();
            noiseGenerator.Clear();
            meshGenerator.Clear();
        }

        public void SaveMap()
        {
            SaveManager.Save(this);
        }
    }
}
