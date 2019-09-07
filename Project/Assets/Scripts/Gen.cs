using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexGen
{
    public abstract class Gen : ScriptableObject
    {
        public abstract void Initialize(Generator generator);
        public abstract void Generate();
    }
}
