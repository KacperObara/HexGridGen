using UnityEngine;

namespace HexGen
{
    public abstract class GridGen : ScriptableObject
    {
        public abstract void Initialize(Generator generator);
        public abstract void Generate();
    }
}
