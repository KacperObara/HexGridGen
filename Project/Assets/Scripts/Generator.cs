using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexGen
{
    public class Generator : MonoBehaviour
    {
        public List<Gen> generators;

        public MapData MapData;
        public MapSettings MapSettings;

        [Space]
        public SaveFile SaveFile;

        public void Generate()
        {
            foreach (Gen generator in generators)
            {
                generator.Initialize(this);
            }

            foreach (Gen generator in generators)
            {
                generator.Generate();
            }
        }

        public void LoadMap()
        {
            if (SaveFile == null)
            {
                Debug.LogWarning("There is no save file to load");
                return;
            }

            MapSettings.Load(SaveManager.Load(SaveFile));
            Generate();
        }

        public void ClearMap()
        {
            GridGenerator g = generators[0] as GridGenerator;
            g.ClearHexes();
        }

        public void SaveMap()
        {
            SaveManager.Save(this);
        }
    }
}
