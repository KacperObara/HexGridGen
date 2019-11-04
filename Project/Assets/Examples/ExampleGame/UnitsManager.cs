using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexGenExampleGame1
{
    [CreateAssetMenu]
    public class UnitsManager : ScriptableObject
    {
        public GameObject playerUnit;
        public GameObject enemyUnit;

        public List<Unit> playerUnits;
        public List<Unit> enemyUnits;

        public List<GameObject> spawnerHexes;

        public Unit SelectedUnit;

        void OnDisable()
        {
            playerUnits.Clear();
            enemyUnits.Clear();
        }
    }
}
