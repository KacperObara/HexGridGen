using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexGen
{
    public class SaveFile : ScriptableObject
    {
        [HideInInspector]
        public string SettingsData;

        [HideInInspector]
        public string EditedMapTextureData;
    }
}
