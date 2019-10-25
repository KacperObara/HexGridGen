using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexGen
{
    public class ShaderManager : MonoBehaviour
    {
        public Texture Texture;
        public int SingleTextureSize;
        
        public int AtlasSize;
        public int TexturesInARow;

        [Space(10)]
        public Shader Shader;
        public Material Material;

        public void UpdateTextureShader()
        {
            Shader.SetGlobalTexture("Texture2D_201B563B", Texture);
            Material.SetTexture("Texture2D_201B563B", Texture);

            if (Texture.width != Texture.height)
            {
                Debug.LogWarning("Texture Width and Height are different, textures won't be applied correctly");
            }

            if (AtlasSize % SingleTextureSize != 0)
            {
                Debug.LogWarning("Single Texture Size is wrong, textures won't be applied correctly");
            }

            AtlasSize = Texture.width;
            TexturesInARow = AtlasSize / SingleTextureSize;
        }
    }
}
