using UnityEngine;

namespace HexGen
{
    public abstract class GridGenerator : ScriptableObject
    {
        public abstract void Initialize(Generator generator);
        public abstract void Generate();
        public abstract void Clear();
    }
}
