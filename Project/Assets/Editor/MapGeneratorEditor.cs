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

            if (GUILayout.Button("Generate"))
            {
                generator.Generate();
            }
        }
    }
}
