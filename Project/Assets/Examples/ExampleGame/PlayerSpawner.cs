using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HexGen;

namespace HexGenExampleGame1
{
    public class PlayerSpawner : MonoBehaviour
    {
        private BoardManager boardManager;
        [SerializeField]
        private SpawnerView spawnerView;

        public int PlayerTanks;
        [HideInInspector]
        public int PlayerTanksLeft;

        void Awake()
        {
            boardManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<BoardManager>();
            PlayerTanksLeft = PlayerTanks;
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
                --PlayerTanksLeft;
                spawnerView.UpdateUnitsLeft();
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
