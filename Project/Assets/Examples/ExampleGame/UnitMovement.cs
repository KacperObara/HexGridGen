using HexGen;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexGenExampleGame1
{
    public class UnitMovement : MonoBehaviour
    {
        public UnitsManager unitsManager;

        public void OnMouseClick(Hex hex)
        {
            if (unitsManager.playerUnits.Contains(unitsManager.SelectedUnit))
            {
                Debug.Log("WEEE");
            }
        }
    }
}

