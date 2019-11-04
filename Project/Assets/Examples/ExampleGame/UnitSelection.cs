using HexGen;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexGenExampleGame1
{
    public class UnitSelection : MonoBehaviour
    {
        public UnitsManager unitsManager;

        public void SelectUnit(Hex hex)
        {
            unitsManager.SelectedUnit = null;

            foreach (Unit unit in unitsManager.playerUnits)
            {
                if (unit.occupiedHex == hex)
                    unitsManager.SelectedUnit = unit;
            }

            foreach (Unit unit in unitsManager.enemyUnits)
            {
                if (unit.occupiedHex == hex)
                    unitsManager.SelectedUnit = unit;
            }
        }
    }
}
