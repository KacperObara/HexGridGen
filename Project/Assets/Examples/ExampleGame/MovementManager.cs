using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HexGen;

namespace HexGenExampleGame1
{
    [RequireComponent(typeof(BoardManager))]
    public class MovementManager : MonoBehaviour
    {
        private BoardManager boardManager;

        private List<GameObject> drawnHexes = new List<GameObject>();

        void Awake()
        {
            boardManager = GetComponent<BoardManager>();
        }

        public void DrawMovementRange()
        {
            ClearDrawnHexes();
            for (int i = 0; i < boardManager.SelectedHexes.Count; i++)
            {
                drawnHexes.Add(Instantiate(boardManager.MovementPrefab, boardManager.SelectedHexes[i].WorldPos, Quaternion.identity));
            }
        }

        public void MoveUnit(Hex hex)
        {
            if (boardManager.IsSelectionPlayerUnit())
            {
                if (boardManager.SelectedHexes.Contains(hex) && boardManager.IsHexEmpty(hex))
                {
                    Unit activeUnit = boardManager.SelectedObject.GetComponent<Unit>();
                    if (activeUnit.Moved == false && activeUnit.OccupiedHex != hex)
                    {
                        boardManager.SelectedObject.transform.position = hex.WorldPos;
                        boardManager.SelectedObject.GetComponent<Unit>().OccupiedHex = hex;
                        boardManager.SelectedHexes.Clear();
                        ClearDrawnHexes();

                        activeUnit.Moved = true;
                    }
                }
            }
        }

        public void ClearDrawnHexes()
        {
            for (int i = 0; i < drawnHexes.Count; i++)
            {
                Destroy(drawnHexes[i].gameObject);
            }
            drawnHexes.Clear();
        }
    }
}
