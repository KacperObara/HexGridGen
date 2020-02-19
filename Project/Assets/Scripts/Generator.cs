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
        private MeshGenerator meshGenerator;

        public GridTexture GridTexture;

        public MapData MapData;
        public MapSettings MapSettings;

        [Space]
        public SaveFile SaveFile;

        private void OnValidate()
        {
            if (GridTexture == null)
            {
                GridTexture = GetComponent<GridTexture>();
                Debug.Log("GridTexture in Generator script was null, loading automatically");
            }

            if (gridGenerator == null || noiseGenerator == null || meshGenerator == null || MapData == null || MapSettings == null)
                Debug.LogError("Generator script misses one or more references. Drag them in the inspector tab");
        }

        public void Generate()
        {
            gridGenerator.Initialize(this);
            noiseGenerator.Initialize(this);
            meshGenerator.Initialize(this);

            gridGenerator.Generate();
            noiseGenerator.Generate();

            UpdateMesh();
        }

        public void UpdateMesh()
        {
            meshGenerator.Initialize(this);
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

            SaveManager.LoadEditedMapTextureData(SaveFile, ref MapData, MapSettings);

            UpdateMesh();
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
