using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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

        public MapData MapData;
        public MapSettings MapSettings;

        [Space]
        public SaveFile SaveFile;


//#if UNITY_EDITOR
//        void Start()
//        {
//            EditorApplication.playModeStateChanged += OnEnteringEditor;
//        }

//        private void OnEnteringEditor(PlayModeStateChange state)
//        {
//            if (state == PlayModeStateChange.EnteredEditMode)
//            {
//                Generator g = this;
//                g.GetComponent<MeshCollider>();
//                UpdateMesh();
//            }
//        }
//#endif

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
