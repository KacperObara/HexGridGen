using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HexGen;
using System.Linq;

namespace HexGenExampleGame1
{
    public class BoardManager : MonoBehaviour
    {
        public GameObject MovementPrefab;
        public GameObject SelectedObject;
        public List<Unit> ExistingUnits;

        public List<GameObject> EnemySpawners;
        public GameObject PlayerPrefab;
        public GameObject EnemyPrefab;
        public GameObject EnemySpawnerPrefab;

        // range of unit movement for example
        public List<Hex> SelectedHexes;

        public List<Unit> GetPlayerUnits()
        {
            return ExistingUnits.Select(x => x).Where(x => x.Faction == Faction.Player).ToList();
        }

        public List<Unit> GetEnemyUnits()
        {
            return ExistingUnits.Select(x => x).Where(x => x.Faction == Faction.Enemy).ToList();
        }

        public bool IsSelectionPlayerUnit()
        {
            if (SelectedObject.GetComponent<Unit>() != null)
            {
                return GetPlayerUnits().Contains(SelectedObject.GetComponent<Unit>());
            }
            return false;
        }

        public bool IsHexEmpty(Hex hex)
        {
            foreach (Unit unit in ExistingUnits)
            {
                if (unit.OccupiedHex == hex)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
