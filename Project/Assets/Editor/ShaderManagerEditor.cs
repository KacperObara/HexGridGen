using UnityEditor;
using UnityEngine;

namespace HexGen
{
    [CustomEditor(typeof(ShaderManager))]
    public class ShaderManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            ShaderManager shaderManager = (ShaderManager)target;

            if (GUILayout.Button("Update Texture Shader"))
            {
                shaderManager.UpdateTextureShader();
            }
        }
    }
}