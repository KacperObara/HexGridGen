using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderManager : MonoBehaviour
{
    public Shader Shader;

    public Texture Texture;

    public void UpdateTextureShader()
    {
        Shader.SetGlobalTexture("Texture2D_201B563B", Texture);
    }
}
