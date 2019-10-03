using UnityEngine;

namespace HexGen
{
    public abstract class MeshGenerator : ScriptableObject
    {
        public abstract void Initialize(Generator generator);
        public abstract void Generate();
        public abstract void Clear();
    }
}
