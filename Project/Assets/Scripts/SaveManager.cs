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
            string jsonMapData = JsonConvert.SerializeObject(generator.MapData.TextureIndex);

            SaveFile saveFile = ScriptableObject.CreateInstance<SaveFile>();
            saveFile.SettingsData = json;
            saveFile.EditedMapTextureData = jsonMapData;

            string path = PickFileName();

            AssetDatabase.CreateAsset(saveFile, path);
        }

        public static MapSettings LoadMapSettings(SaveFile saveFile)
        {
            MapSettings settings = ScriptableObject.CreateInstance<MapSettings>();

            JsonConvert.PopulateObject(saveFile.SettingsData, settings);
            return settings;
        }

        public static void LoadEditedMapTextureData(SaveFile saveFile, ref MapData mapData)
        {
            int[] indexes = new int[mapData.TextureIndex.Length];
            JsonConvert.PopulateObject(saveFile.EditedMapTextureData, indexes);
            JsonConvert.PopulateObject(saveFile.EditedMapTextureData, mapData.TextureIndex);

            Debug.Log("D: " + indexes[0]);

            //MapData mapData = ScriptableObject.CreateInstance<MapData>();

            //JsonConvert.PopulateObject(saveFile.EditedMapTextureData, mapData);
            //return mapData;
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
