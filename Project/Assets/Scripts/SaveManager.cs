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

        private static JsonSerializerSettings serializerSettings = new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace };

        static SaveManager()
        {
            if (!Directory.Exists(SAVE_FOLDER))
                Directory.CreateDirectory(SAVE_FOLDER);

            JsonConvert.DefaultSettings().Converters.Add(new ColorConverter());
        }

        public static void Save(Generator generator)
        {
            string json = JsonConvert.SerializeObject(generator.MapSettings);
            string jsonMapData = JsonConvert.SerializeObject(new TextureDataObject(generator.MapData.GetAllTerrainIndexes()));

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

        public static void LoadEditedMapTextureData(SaveFile saveFile, ref MapData mapData, MapSettings mapSettings)
        {
            TextureDataObject tempObject = new TextureDataObject(new int[mapData.Hexes.Length]);
            JsonConvert.PopulateObject(saveFile.EditedMapTextureData, tempObject, serializerSettings);

            mapData.LoadTerrainData(mapSettings, tempObject.Indexes);
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

        /// <summary>
        /// Temporary class holding texture indexes, because JsonConvert.PopulateObject
        /// cannot serialize array of primitives
        /// </summary>
        private class TextureDataObject
        {
            public int[] Indexes;
            public TextureDataObject(int[] indexes)
            {
                this.Indexes = indexes;
            }
        }
    }
}
