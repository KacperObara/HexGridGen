using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HexGen;

namespace HexGenExampleGame1
{
    public class TurnManager : MonoBehaviour
    {
        private BoardManager boardManager;
        private EnemySpawner enemySpawner;

        private bool ChangeTurn = false;

        void Awake()
        {
            boardManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<BoardManager>();
            enemySpawner = GameObject.FindGameObjectWithTag("GameManager").GetComponent<EnemySpawner>();
        }

        void Update()
        {
            if (ChangeTurn == true)
            {
                bool isEveryoneStopped = true;

                foreach (Unit unit in boardManager.GetEnemyUnits())
                {
                    if (unit.Moved == false)
                    {
                        isEveryoneStopped = false;
                        break;
                    }
                }

                if (isEveryoneStopped == true)
                {
                    foreach (Unit unit in boardManager.ExistingUnits)
                    {
                        unit.Moved = false;
                        unit.Shot = false;
                    }
                    ChangeTurn = false;
                }
            }
        }

        public void SwitchTurn()
        {
            ChangeTurn = true;
        }

        /// <summary>
        /// Allows for turn switching only, if enemy units are not moving
        /// </summary>
        public void TrySwitchTurn()
        {
            foreach (Unit unit in boardManager.GetEnemyUnits())
            {
                if (unit.GetComponent<EnemyBehaviour>().Moving == true)
                {
                    return;
                }
            }
            enemySpawner.SpawnEnemies();
            SwitchTurn();
        }
    }
}
