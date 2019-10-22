using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderManager : MonoBehaviour
{
    public Texture Texture;

    [Space(10)]
    public Shader Shader;
    public Material Material;

    public void UpdateTextureShader()
    {
        Shader.SetGlobalTexture("Texture2D_201B563B", Texture);
        Material.SetTexture("Texture2D_201B563B", Texture);
    }
}
