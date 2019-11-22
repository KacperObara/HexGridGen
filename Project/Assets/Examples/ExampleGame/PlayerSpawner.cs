using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HexGen;

namespace HexGenExampleGame1
{
    public class PlayerSpawner : MonoBehaviour
    {
        private BoardManager boardManager;

        public int PlayerTanks;

        void Awake()
        {
            boardManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<BoardManager>();
        }

        public void OnHexClick(Hex hex)
        {
            if (boardManager.GetPlayerUnits().Count < PlayerTanks)
            {
                foreach (Unit unit in boardManager.ExistingUnits)
                {
                    if (unit.OccupiedHex.WorldPos == hex.WorldPos)
                    {
                        return;
                    }
                }
                SpawnTank(hex);
            }
        }

        private void SpawnTank(Hex hex)
        {
            Unit newTank = Instantiate(boardManager.PlayerPrefab, hex.WorldPos, Quaternion.identity).GetComponent<Unit>();
            newTank.Initialize(hex, Faction.Player);
            boardManager.ExistingUnits.Add(newTank);
        }
    }
}
