using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Converters;
using UnityEditor;

namespace HexGen
{
    public class SaveManager
    {
        private static readonly string SAVE_FOLDER = Application.dataPath + "/Saves/";

        static SaveManager()
        {
            if (!Directory.Exists(SAVE_FOLDER))
                Directory.CreateDirectory(SAVE_FOLDER);

            JsonConvert.DefaultSettings().Converters.Add(new ColorConverter());
        }

        public static void Save(Generator generator)
        {
            string json = JsonConvert.SerializeObject(generator.MapSettings);

            SaveFile saveFile = ScriptableObject.CreateInstance<SaveFile>();
            saveFile.SettingsData = json;

            string path = PickFileName();

            AssetDatabase.CreateAsset(saveFile, path);
        }

        public static MapSettings Load(SaveFile saveFile)
        {
            MapSettings settings = ScriptableObject.CreateInstance<MapSettings>();

            JsonConvert.PopulateObject(saveFile.SettingsData, settings);
            return settings;
        }

        private static string PickFileName()
        {
            string path = "Assets/Saves/Save.asset";
            if (File.Exists(path))
            {
                int i = 1;
                while (File.Exists($"Assets/Saves/Save({i}).asset"))
                {
                    ++i;
                }
                path = $"Assets/Saves/Save({i}).asset";
            }
            return path;
        }
    }
}
