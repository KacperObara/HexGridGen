using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HexGen;

namespace HexGenExampleGame1
{
    public class PlayerSpawner : MonoBehaviour
    {
        public UnitsManager unitsManager;

        public int PlayerTanks;

        public void OnHexClick(Hex hex)
        {
            if (unitsManager.playerUnits.Count < PlayerTanks)
            {
                SpawnTank(hex);
            }
        }

        private void SpawnTank(Hex hex)
        {
            Unit newTank = Instantiate(unitsManager.playerUnit, hex.WorldPos, Quaternion.identity).GetComponent<Unit>();
            newTank.Initialize(hex, 5);
            unitsManager.playerUnits.Add(newTank);
        }
    }
}
