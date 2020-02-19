using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexGen
{
    public class ShaderManager : MonoBehaviour
    {
        public Texture Texture;
        
        [SerializeField]
        private int atlasSize;
        public int AtlasSize
        {
            get { return atlasSize; }
            set { atlasSize = value; }
        }

        [SerializeField]
        private int texturesInARow;
        public int TexturesInARow
        {
            get { return texturesInARow; }
            set { texturesInARow = value; }
        }

        public int SingleTextureSize { get; private set; }

        [Space(10)]
        public Shader Shader;
        public Material Material;

        public void UpdateTextureShader()
        {
            Shader.SetGlobalTexture("Texture2D_201B563B", Texture);
            Material.SetTexture("Texture2D_201B563B", Texture);

            SingleTextureSize = AtlasSize / TexturesInARow;

            if (Texture.width != Texture.height)
            {
                Debug.LogWarning("Texture Width and Height are different, textures won't be applied correctly");
            }

            if (AtlasSize % SingleTextureSize != 0)
            {
                Debug.LogWarning("Single Texture Size is wrong, textures won't be applied correctly");
            }
        }
    }
}
