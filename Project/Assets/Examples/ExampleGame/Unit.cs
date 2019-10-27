using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HexGen;

namespace HexGenExampleGame1
{
    public class Unit : MonoBehaviour, IMovable
    {
        public int range;
        public Hex occupiedHex;

        public int Range { get => range; set => range = value; }
        public Hex OccupiedHex { get => occupiedHex; set => occupiedHex = value; }

        public void Initialize(Hex occupiedHex, int range)
        {
            this.OccupiedHex = occupiedHex;
            this.Range = range;
        }
    }
}
