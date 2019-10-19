using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexGen
{
    public interface IMovable
    {
        int Range { get; set; }
        Hex OccupiedHex { get; set; }
    }
}