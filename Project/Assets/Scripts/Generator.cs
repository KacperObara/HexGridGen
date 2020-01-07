using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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

        public ShaderManager shaderManager;

        public MapData MapData;
        public MapSettings MapSettings;

        [Space]
        public SaveFile SaveFile;


        //#if UNITY_EDITOR
        //        void Start()
        //        {
        //            DontDestroyOnLoad(this.gameObject);
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
            Stopwatch sw = new Stopwatch();
            sw.Start();

            gridGenerator.Initialize(this);
            noiseGenerator.Initialize(this);
            meshGenerator.Initialize(this);

            gridGenerator.Generate();
            noiseGenerator.Generate();

            UpdateMesh();

            sw.Stop();

            UnityEngine.Debug.Log(sw.Elapsed.TotalMilliseconds);
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
                UnityEngine.Debug.LogWarning("There is no save file to load");
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
