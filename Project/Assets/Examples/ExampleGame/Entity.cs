using HexGen;
using UnityEngine;

namespace HexGenExampleGame1
{
    public class Entity : MonoBehaviour, IEntity
    {
        public Hex OccupiedHex { get; set; }
    }
}
