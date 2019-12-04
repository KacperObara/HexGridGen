using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HexGen;
using UnityEditor;

namespace HexGenExampleGame1
{
    public class Unit : MonoBehaviour, IMovable
    {
        private BoardManager boardManager;

        [SerializeField]
        private int range;

        public int Range { get { return range; } }
        public Hex OccupiedHex { get; set; }
        public Faction Faction { get; set; }
        public bool Moved { get; set; }
        public bool Shot { get; set; }

        void Awake()
        {
            boardManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<BoardManager>();
        }

        public void Initialize(Hex occupiedHex, Faction faction)
        {
            this.OccupiedHex = occupiedHex;
            this.Faction = faction;
        }

        void OnMouseUp()
        {
            boardManager.SelectedObject = this.gameObject;
            if (boardManager.GetPlayerUnits().Contains(this))
            {
                // selects hex under the tank to show selection
                boardManager.SelectedHexes.Clear();
                boardManager.SelectedHexes.Add(this.OccupiedHex);
                if (Moved == false)
                {
                    boardManager.SelectedHexes = HexRange.GetHexesInRange(this.OccupiedHex, this);
                    boardManager.SelectedHexes.Remove(this.OccupiedHex);
                }
                MovementManager movementManager = boardManager.GetComponent<MovementManager>();
                movementManager.DrawMovementRange();
            }
        }
    }
}
