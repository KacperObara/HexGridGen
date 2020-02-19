using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HexTextureAtlas : ScriptableObject
{
    public Texture Texture;
    public Material Material;

    [Tooltip ("How many textures fit in a row (1, 2, 4, 9, 16...)")]
    [SerializeField]
    private int texturesInARow;
    public int TexturesInARow
    {
        get { return texturesInARow; }
        set { texturesInARow = value;
              UpdateTextureInfo();   }
    }

    [HideInInspector]
    public int AtlasSize { get; set; }
    [HideInInspector]
    public int SingleTextureSize { get; set; }

    private void OnEnable()
    {
        Shader.SetGlobalTexture("Texture2D_201B563B", Texture);
        Material.SetTexture("Texture2D_201B563B", Texture);
        AtlasSize = Texture.width;
        SingleTextureSize = AtlasSize / TexturesInARow;
    }

    private void UpdateTextureInfo()
    {
        AtlasSize = Texture.width;
        SingleTextureSize = AtlasSize / TexturesInARow;
    }
}
