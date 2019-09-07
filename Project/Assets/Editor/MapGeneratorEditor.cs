using UnityEditor;
using UnityEngine;

namespace HexGen
{
    [CustomEditor(typeof(Generator))]
    public class MapGeneratorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            Generator generator = (Generator)target;

            if (GUILayout.Button("Save map"))
            {
                generator.SaveMap();
            }
            if (GUILayout.Button("Load map"))
            {
                generator.LoadMap();
            }
            GUILayout.Space(12);
            if (GUILayout.Button("Generate"))
            {
                generator.Generate();
            }
            GUILayout.Space(12);
            if (GUILayout.Button("Clear map"))
            {
                generator.ClearMap();
            }
        }
    }
}
