using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HexGen;

namespace HexGenExampleGame1
{
    public class TurnManager : MonoBehaviour
    {
        private BoardManager boardManager;
        private bool ChangeTurn = false;

        void Awake()
        {
            boardManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<BoardManager>();
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
                    }
                    ChangeTurn = false;
                }
            }
        }

        public void SwitchTurn()
        {
            ChangeTurn = true;
        }
    }
}
